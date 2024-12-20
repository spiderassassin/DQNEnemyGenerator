using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameActionBasedAgent: FSMAgent
{
    private int frameCount;
    public GameHandler gameHandler;
    public float reward;
    private bool tookIllegalAction;
    

    // Every X frames takes an action.
    public int actionFrequency = 1;

    void Start()
    {
        gameHandler = FindAnyObjectByType<GameHandler>();
        reward = gameHandler.currReward;
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
        }
        Vector3 nextNode = GetPosition() + direction * Config.AGENT_MOVE_INTERVAL;
        return LegalAction(nextNode);
    }

    public override bool TookIllegalAction()
    {
        if (tookIllegalAction)
        {
            tookIllegalAction = false;
            return true;
        }
        return false;
    }

    // Move towards the next node in the specified direction.
    private void Move(Vector3 direction)
    {
        // Get closest node to the current position + 1 grid interval in the direction of the action.
        Vector3 nextNode = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(GetPosition() + direction * Config.GRID_INTERVAL).Location;
        if (LegalAction(nextNode))
        {
            // Get corrected direction.
            // Vector3 target = ObstacleHandler.Instance.GetCorrectedTarget(direction, nextNode);
            // SetTarget(target);
            SetTarget(nextNode);
        }
        else
        {
            // Don't do anything.
            SetTarget(HW3NavigationHandler.Instance.NodeHandler.ClosestNode(GetPosition()).Location);
            tookIllegalAction = true;
        }
    }
}
