using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameActionBasedAgent: FSMAgent
{
    private int frameCount;

    // Every X frames takes an action.
    public int actionFrequency = 1;

    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        currState = new FrameActionState();
        currState.EnterState(this);
        frameCount = 0;
    }

    public override void TakeAction(Action action)
    {
        if (frameCount == 0)
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
        frameCount = (frameCount + 1) % actionFrequency;
    }

    // Check if the next node in the direction of the action is on the path.
    public override bool LegalAction(Vector3 nextNode)
    {
        return ObstacleHandler.Instance.CheckPointOnPath(new Vector2(nextNode.x, nextNode.y), new Vector2(GetPosition().x, GetPosition().y));
    }

    public override bool LegalAction(Action action)
    {
        Vector3 direction = Vector3.zero;
        switch (action)
        {
            case Action.Up:
                direction = Vector3.up;
                break;
            case Action.Down:
                direction = Vector3.down;
                break;
            case Action.Left:
                direction = Vector3.left;
                break;
            case Action.Right:
                direction = Vector3.right;
                break;
            case Action.Stay:
                return true;
        }
        Vector3 nextNode = GetPosition() + direction * Config.AGENT_MOVE_INTERVAL;
        return LegalAction(nextNode);
    }

    // Move towards the next node in the specified direction.
    private void Move(Vector3 direction)
    {
        Vector3 nextNode = GetPosition() + direction * Config.AGENT_MOVE_INTERVAL;
        if (LegalAction(nextNode))
        {
            // Get corrected direction.
            Vector3 target = ObstacleHandler.Instance.GetCorrectedTarget(direction, nextNode);
            SetTarget(target);
        }
        else
        {
            TakeAction(Action.Stay);
        }
    }
}
