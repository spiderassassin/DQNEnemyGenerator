using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class mlstuff : Agent
{
    public FrameActionBasedAgent agent;
    public GameHandler gameHandler;
    public float reward;
    public GameHandler.State state;


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
        sensor.AddObservation(state.agentPosition);
        sensor.AddObservation(state.ghostPosition);
        sensor.AddObservation(state.score);
        sensor.AddObservation(state.pelletPositions.Length);
        sensor.AddObservation(state.gameOver);
        sensor.AddObservation(state.wallUp);
        sensor.AddObservation(state.wallDown);
        sensor.AddObservation(state.wallLeft);
        sensor.AddObservation(state.wallRight);
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    private void LateUpdate()
    {
        gameHandler.UpdateState();
        reward = gameHandler.currReward;
        state = gameHandler.GetState();
        AddReward(reward);
        if(state.gameOver == true || gameHandler.timestep>= 60000)
        {
            // Add this back when we start trying with final reward.
            // AddReward(reward);
            EndEpisode();
        }
        
        //print(state);

    }
}
