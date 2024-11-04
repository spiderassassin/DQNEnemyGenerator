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
        action = FrameActionBasedAgent.Action.Right;
        // Take the action.
        agent.TakeAction(action);

        // Stay in this state.
        return this;
    }

    public override void EnterState(FSMAgent agent){ }

    public override void ExitState(FSMAgent agent){ }
}
