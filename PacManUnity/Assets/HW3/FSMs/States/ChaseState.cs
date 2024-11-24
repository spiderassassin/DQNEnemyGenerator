using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    //Path
    private Vector3[] path;
    private int pathIndex;
    private Vector3 currTarget;

    //Set name of this state
    public ChaseState():base("Chase"){ }

    public override State Start(FSMAgent agent)
    {
        Vector3 currPos = agent.transform.position;
        currPos += new Vector3(0.1f, 0, 0);
        agent.SetTarget(currPos);
        currTarget = agent.transform.position;

        return this;
    }

    public override State Update(FSMAgent agent)
    {
        // Check if close enough to eat pacman.
        Vector3 pacmanLocation = PacmanInfo.Instance.transform.position;
        if (agent.CloseEnough(pacmanLocation))
        {
            ScoreHandler.Instance.KillPacman();
        }

        //Follow Pacman
        GraphNode closestStart = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(agent.transform.position);
        GraphNode closestGoal = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(pacmanLocation);
        path = HW3NavigationHandler.Instance.PathFinder.CalculatePath(closestStart, closestGoal);

        if (path == null || path.Length < 1)
        {
            // Do nothing.
        }
        else
        {
            pathIndex = 0;
            // The target should be the next node in the path only if we've reached the center of the current node.
            Vector3 distBetweenTarget = agent.transform.position - currTarget;
            if (Mathf.Abs(distBetweenTarget.x) < 0.0001f && Mathf.Abs(distBetweenTarget.y) < 0.0001f)
            {
                currTarget = path[pathIndex];
            }
            agent.SetTarget(currTarget);
        }

        //Stay in this state
        return this;
    }

    public override void EnterState(FSMAgent agent){ }

    public override void ExitState(FSMAgent agent){ }
}
