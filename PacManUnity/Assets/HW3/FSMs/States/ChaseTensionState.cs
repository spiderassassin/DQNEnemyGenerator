using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTensionState : State
{
    //Path
    private Vector3[] path;
    private int pathIndex;
    public Vector3 currTarget;

    //Set name of this state
    public ChaseTensionState():base("ChaseTension"){ }

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

        //Follow Pacman but target a node 2 nodes away from pacman, closest to the ghost.
        GraphNode closestStart = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(agent.transform.position);
        GraphNode closestGoal = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(pacmanLocation);

        // Get the path to the closest node to pacman that is 2 nodes away from pacman.
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
                // But also restrict the target to be 2 nodes away from pacman.
                if (path.Length == 2)
                {
                    currTarget = agent.transform.position;
                }
                else
                {
                    currTarget = path[pathIndex];
                }
            }
            agent.SetTarget(currTarget);
        }

        //Stay in this state
        return this;
    }

    public override void EnterState(FSMAgent agent){ }

    public override void ExitState(FSMAgent agent){ }
}
