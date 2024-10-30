using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBasedAgent: FSMAgent
{
    // Length of time agent spends executing each action. Changing this effectively changes the speed of the agent.
    public float turn_length = 1.0f;

    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        currState = new ActionState();
        currState.EnterState(this);
    }

    public override void TakeAction(Action action)
    {
        switch (action)
        {
            case Action.Up:
                if (LegalAction(Vector3.up)) { Move(Vector3.up); }
                else { TakeAction(Action.Stay); }
                break;
            case Action.Down:
                if (LegalAction(Vector3.down)) { Move(Vector3.down); }
                else { TakeAction(Action.Stay); }
                break;
            case Action.Left:
                if (LegalAction(Vector3.left)) { Move(Vector3.left); }
                else { TakeAction(Action.Stay); }
                break;
            case Action.Right:
                if (LegalAction(Vector3.right)) { Move(Vector3.right); }
                else { TakeAction(Action.Stay); }
                break;
            case Action.Stay:
                Stay();
                break;
        }
    }

    // Check if the next node in the direction of the action is a wall or not.
    private bool LegalAction(Vector3 direction)
    {
        Vector3 nextNode = GetPosition() + direction * GridHandler.gridInterval;
        return !ObstacleHandler.Instance.PointInObstacles(nextNode);
    }

    // Do nothing for turn_length duration.
    private void Stay()
    {
        SetTimer(turn_length);
    }

    // Move to the next node in the specified direction over a duration of turn_length.
    private void Move(Vector3 direction)
    {
        SetTarget(GetPosition() + direction * GridHandler.gridInterval, turn_length);
    }
}
