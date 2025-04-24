using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class mlstuff : Agent
{
    public FSMAgent agent;
    public GameHandler gameHandler;
    public float reward;
    public GameHandler.State state;

    [SerializeField] private bool demonstration;
    [SerializeField] private int maxEpisodes = 0;

    private int episodes;

    void Start()
    {
        //print(agent.action);
        gameHandler = FindAnyObjectByType<GameHandler>();
        
    }

    public override void OnEpisodeBegin()
    {
        gameHandler.ResetGame();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(state.agentPositionIndex);
        sensor.AddObservation(state.ghostPositionIndex);
        //for(int i = 0; i < state.pelletPositions.Length; i++)
        //{
        //sensor.AddObservation(state.pelletPositions[i]);
        //}




    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        agent.action = actions.DiscreteActions[0];
        print("Action: " + agent.action);
    }
    
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // If demonstration, get the action manually, otherwise don't change anything.
        if (demonstration)
        {
            var actions = actionsOut.DiscreteActions;
            actions[0] = gameHandler.GetCurrentAction();
        }
    }

    private void LateUpdate()
    {
        gameHandler.UpdateState();
        reward = gameHandler.currReward;
        state = gameHandler.GetState();
        if (state.waitingForAction)
        {
            AddReward(reward);
            RequestDecision();
        }
        if(state.gameOver == true || gameHandler.timestep>= 60000)
        {
            // Add this back when we start trying with final reward.
            // AddReward(reward);
            EndEpisode();
            episodes += 1;
            // End after a certain number of episodes.
            if (maxEpisodes != 0 && episodes >= maxEpisodes)
            {
                print("Reached max episodes");
                // Quit the game.
                Academy.Instance.Dispose();
                Application.Quit();
            }
        }
    }
}
