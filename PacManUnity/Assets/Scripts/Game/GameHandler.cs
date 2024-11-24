using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler: MonoBehaviour
{
    public PelletHandler pelletHandler;

    public class State
    {
        public Vector3 agentPosition;
        public bool wallUp;
        public bool wallDown;
        public bool wallLeft;
        public bool wallRight;
        public Vector3 ghostPosition;
        public Vector3[] pelletPositions;
        public int score;
        public bool gameOver;
        public float reward;
    }

    public float currReward;
    public float accTension;
    public float timestep;

    void Start()
    {

        HW3NavigationHandler.Instance.Init();

        List<GraphNode> corners = new List<GraphNode>();

        float[] XBounds = ObstacleHandler.Instance.GetPathXBounds();
        float[] YBounds = ObstacleHandler.Instance.GetPathYBounds();
        foreach (float x in XBounds)
        {
            foreach (float y in YBounds)
            {
                GraphNode g = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(new Vector3(x, y));
                corners.Add(g);
            }
        }

        // Add pellets along the path.
        SpawnPellets();

        currReward = 0;
        accTension = 0;
        timestep = 0;
    }

    public void SpawnPellets()
    {
        Vector2[][] path = ObstacleHandler.Instance.GetWalkablePath();
        // In the future, can change to true if we implement power pellets.
        bool powerPellets = false;
        foreach (Vector2[] line in path)
        {
            Vector3 pos = new Vector3(line[0].x, line[0].y, 0);
            GraphNode g = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(pos);
            pelletHandler.AddPellet(g.Location);
        }
    }

    private void LateUpdate()
    {
        // UpdateState();
    }

    public void UpdateState()
    {
        // Check for any tension updates (i.e. 1 if pacman is within certain path distance of ghost, else 0).
        if (GhostManager.Instance.GhostsInPlay.Length != 1)
        {
            //Debug.LogError("There should be exactly one ghost in play.");
        }
        else
        {
            Vector3 agentPos = PacmanInfo.Instance.transform.position;
            Vector3 ghostPos = GhostManager.Instance.GhostsInPlay[0].GetPosition();
            // Calculate path between agent and ghost.
            GraphNode closestNodeAgent = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(agentPos);
            GraphNode closestNodeGhost = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(ghostPos);
            Vector3[] path = HW3NavigationHandler.Instance.PathFinder.CalculatePath(closestNodeAgent, closestNodeGhost);
            // Check if path is within certain distance.
            accTension += path.Length < Config.TENSION_DISTANCE ? 1 : 0;
            // currReward = path.Length < Config.TENSION_DISTANCE ? 1 : 0;
            // Just give -1 reward to make things faster.
            currReward = -1;
            // Also give -1 if agent hits the wall.
            currReward += GhostManager.Instance.GhostsInPlay[0].TookIllegalAction() ? -1 : 0;
        }

        // Update the reward if game is over.
        if (Time.timeScale == 0)
        {
            // Length of time taken to complete the game (penalize for longer time).
            // Number of pellets remaining (penalize for more remaining).
            // Average tension (follow a normal distribution).
            float tensionReward = CalculateTensionReward(Config.TENSION_MEAN, Config.TENSION_STD_DEV);
            // Add this back later perhaps.
            // currReward += tensionReward - timestep - pelletHandler.NumPellets;
            // For now, just set reward as killing pacman.
            currReward = pelletHandler.NumPellets > 0 ? 1000 : 0;
        }

        timestep += 1;
    }

    private float CalculateTensionReward(float mean, float stdDev)
    {
        float avgTension = accTension / timestep;
        // Using formula for normal distribution.
        return (float) Math.Exp(-Math.Pow(avgTension - mean, 2) / (2 * Math.Pow(stdDev, 2)));
    }

    // Get the current state of the game.
    public State GetState()
    {
        State state = new State();

        // Get the positions of the agent and the ghost.
        state.agentPosition = PacmanInfo.Instance.transform.position;
        FSMAgent[] ghosts = GhostManager.Instance.GhostsInPlay;
        if (ghosts.Length != 1)
        {
            Debug.LogError("There should be exactly one ghost in play.");
        }
        else
        {
            state.ghostPosition = ghosts[0].GetPosition();
        }

        // Get whether the tile in each direction is a wall.
        state.wallUp = ObstacleHandler.Instance.CheckPointOnPath(new Vector2(state.agentPosition.x, state.agentPosition.y + Config.GRID_INTERVAL), new Vector2(state.agentPosition.x, state.agentPosition.y));
        state.wallDown = ObstacleHandler.Instance.CheckPointOnPath(new Vector2(state.agentPosition.x, state.agentPosition.y - Config.GRID_INTERVAL), new Vector2(state.agentPosition.x, state.agentPosition.y));
        state.wallLeft = ObstacleHandler.Instance.CheckPointOnPath(new Vector2(state.agentPosition.x - Config.GRID_INTERVAL, state.agentPosition.y), new Vector2(state.agentPosition.x, state.agentPosition.y));
        state.wallRight = ObstacleHandler.Instance.CheckPointOnPath(new Vector2(state.agentPosition.x + Config.GRID_INTERVAL, state.agentPosition.y), new Vector2(state.agentPosition.x, state.agentPosition.y));

        // Get the positions of the pellets.
        state.pelletPositions = pelletHandler.GetPelletPositions();

        // Get the current score.
        state.score = ScoreHandler.Instance.Score;

        // Get whether the game is over.
        state.gameOver = Time.timeScale == 0;

        // Get the current reward.
        state.reward = currReward;

        return state;
    }

    public int GetCurrentAction()
    {
        return GhostManager.Instance.GhostsInPlay[0].GetCurrentAction();
    }

    public void ResetState()
    {
        currReward = 0;
        accTension = 0;
        timestep = 0;
    }

    public void ResetGame()
    {
        // Reset the positions of the agent and the ghost.
        PacmanInfo.Instance.ResetPosition();
        GhostManager.Instance.ResetGhosts();

        // Reset the positions of the pellets.
        pelletHandler.ResetPellets();
        SpawnPellets();

        // Reset the score.
        ScoreHandler.Instance.ResetScore();

        // Reset the state.
        ResetState();
        // Reset timescale to normal.
        Time.timeScale = 1;
    }
}
