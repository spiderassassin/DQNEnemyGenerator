using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameActionBasedAgent: FSMAgent
{
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
                Move(Vector3.up);
                break;
            case Action.Down:
                Move(Vector3.down);
                break;
            case Action.Left:
                Move(Vector3.left);
                break;
            case Action.Right:
                Move(Vector3.right);
                break;
            case Action.Stay:
                // Do nothing.
                break;
        }
    }

    // Check if the next node in the direction of the action is on the path.
    private bool LegalAction(Vector3 direction, Vector3 nextNode)
    {
        return ObstacleHandler.Instance.CheckPointOnPath(new Vector2(nextNode.x, nextNode.y));
    }

    // Move towards the next node in the specified direction.
    private void Move(Vector3 direction)
    {
        Vector3 nextNode = GetPosition() + direction * Config.AGENT_MOVE_INTERVAL;
        if (LegalAction(direction, nextNode))
        {
            Debug.Log("Moving " + direction);
            Debug.Log("Next node: " + nextNode);
            // Get corrected direction.
            Vector3 target = ObstacleHandler.Instance.GetCorrectedTarget(direction, nextNode);
            SetTarget(GetPosition() + direction * Config.GRID_INTERVAL);
        }
        else
        {
            TakeAction(Action.Stay);
        }
    }
}
