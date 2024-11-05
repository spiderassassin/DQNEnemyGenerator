using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameActionState : State
{
    // Set name of this state.
    public FrameActionState():base("FrameAction"){ }

    public override State Update(FSMAgent agent)
    {
        // Pick randomly for now.
        FrameActionBasedAgent.Action action = (FrameActionBasedAgent.Action)Random.Range(0, 5);
        while (true) {
            if (action == FrameActionBasedAgent.Action.Stay) {
                break;
            }
            // Convert action to direction vector.
            Vector3 direction = Vector3.zero;
            switch (action) {
                case FrameActionBasedAgent.Action.Up:
                    direction = Vector3.up;
                    break;
                case FrameActionBasedAgent.Action.Down:
                    direction = Vector3.down;
                    break;
                case FrameActionBasedAgent.Action.Left:
                    direction = Vector3.left;
                    break;
                case FrameActionBasedAgent.Action.Right:
                    direction = Vector3.right;
                    break;
            }
            // Only break if the action is legal.
            if (agent.LegalAction(direction) && action != FrameActionBasedAgent.Action.Left) {
                break;
            }
            else
            {
                action = (FrameActionBasedAgent.Action)Random.Range(0, 5);
            }
        }
        // action = FrameActionBasedAgent.Action.Right;
        // Take the action.
        agent.TakeAction(action);

        // Stay in this state.
        return this;
    }

    public override void EnterState(FSMAgent agent){ }

    public override void ExitState(FSMAgent agent){ }
}
