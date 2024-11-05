using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameActionBasedAgent: FSMAgent
{
    private float agentSize = 0.1f;
    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        currState = new FrameActionState();
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
                // Do nothing.
                break;
        }
    }

    // Check if the next node in the direction of the action is a wall or not.
    public override bool LegalAction(Vector3 direction)
    {
        Vector3 currentNode = GetPosition();
        Vector3 nextNodeMin = GetPosition() + direction * 2 * GridHandler.gridInterval;
        Vector3 agentSizeVector;
        if (direction == Vector3.up || direction == Vector3.down)
        {
            agentSizeVector = new Vector3(agentSize, 0, 0);
        }
        else
        {
            agentSizeVector = new Vector3(0, agentSize, 0);
        }
        Vector3 nextNodeMax = GetPosition() - agentSizeVector + direction * 2 * GridHandler.gridInterval;
        return (!ObstacleHandler.Instance.PointInObstacles(nextNodeMin, currentNode) && !ObstacleHandler.Instance.PointInObstacles(nextNodeMax, currentNode));
    }

    // Move towards the next node in the specified direction.
    private void Move(Vector3 direction)
    {
        SetTarget(GetPosition() + direction * GridHandler.gridInterval);
    }
}
