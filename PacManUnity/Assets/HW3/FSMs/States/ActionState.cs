using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : State
{
    //Set name of this state
    public ActionState():base("Action"){ }

    public override State Update(FSMAgent agent)
    {
        if (!agent.TimerComplete()) return this;

        // Pick randomly for now.
        ActionBasedAgent.Action action = (ActionBasedAgent.Action)Random.Range(0, 5);
        // Take the action.
        agent.TakeAction(action);

        // Stay in this state.
        return this;
    }

    public override void EnterState(FSMAgent agent){ }

    public override void ExitState(FSMAgent agent){ }
}
