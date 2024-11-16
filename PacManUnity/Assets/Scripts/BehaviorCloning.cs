using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class BehaviorCloning : Agent
{
    public ArrowKeysAgent pacman;
    public GameHandler gameHandler;
    public GameHandler.State state;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //print(agent.action);
        gameHandler = FindAnyObjectByType<GameHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        state = gameHandler.GetState();

        if (state.gameOver == true || gameHandler.timestep >= 60000)
        {
            EndEpisode();
        }


    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(state.agentPosition);
        sensor.AddObservation(state.ghostPosition);
        sensor.AddObservation(state.score);
        sensor.AddObservation(state.pelletPositions.Length);
        sensor.AddObservation(state.gameOver);
        //for(int i = 0; i < state.pelletPositions.Length; i++)
        //{
        //sensor.AddObservation(state.pelletPositions[i]);
        //}*/




    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        pacman.nextDirection = actions.DiscreteActions[0];
        /* agent.action = actions.DiscreteActions[0];
         print("Action: " + agent.action);*/
    }
}
