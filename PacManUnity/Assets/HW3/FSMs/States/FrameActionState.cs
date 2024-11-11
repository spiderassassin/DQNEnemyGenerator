using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameActionState : State
{
    // Set name of this state.
    public FrameActionState():base("FrameAction"){ }

    public override State Update(FSMAgent agent)
    {
        // Check if close enough to eat pacman.
        Vector3 pacmanLocation = PacmanInfo.Instance.transform.position;
        if (agent.CloseEnough(pacmanLocation))
        {
            ScoreHandler.Instance.KillPacman();
        }

        // Pick randomly for now.
        // FrameActionBasedAgent.Action action = (FrameActionBasedAgent.Action)Random.Range(0, 5);

        // More intelligent version, only pick from legal actions.
        List<int> possibleActions = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            if (agent.LegalAction((FrameActionBasedAgent.Action)i))
            {
                possibleActions.Add(i);
            }
        }
        System.Random rnd = new System.Random();
        int index = rnd.Next(possibleActions.Count);
        FrameActionBasedAgent.Action action = (FrameActionBasedAgent.Action)possibleActions[index];

        // Take the action.
        agent.TakeAction(action);

        // Stay in this state.
        return this;
    }

    public override void EnterState(FSMAgent agent){ }

    public override void ExitState(FSMAgent agent){ }
}
