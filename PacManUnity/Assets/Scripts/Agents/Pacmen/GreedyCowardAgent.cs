﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyCowardAgent : MonoBehaviour
{
    private Vector3 target;
    private bool movingTowardTarget;
    public bool MovingTowardTarget { get { return movingTowardTarget; } }

    private float speed = AgentConstants.SPEED;

    //Path
    private Vector3[] path;
    private int pathIndex;
    private Vector3 currTarget;

    public void SetTarget(Vector3 _target)
    {
        target = _target;
        if (Mathf.Abs(transform.position.x - target.x) < AgentConstants.EPSILON)//Special case moving vertically
        {
            Vector3 eulerAngles = transform.eulerAngles;
            if (transform.position.y < target.y)
            {
                eulerAngles.x = -90;
            }
            else
            {
                eulerAngles.x = 90;
            }
            transform.eulerAngles = eulerAngles;
        }
        else
        {
            transform.LookAt(target, Vector3.up);
        }
        movingTowardTarget = true;
    }

     void Start()
    {
        Vector3 currPos = transform.position;
        currPos += new Vector3(0.1f, 0, 0);
        SetTarget(currPos);
        currTarget = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bool runningAway = false;
        FSMAgent ghost = GhostManager.Instance.GetClosestGhost(transform.position);
        if (ghost != null)
        {
            Vector3 vecToGhost = ghost.GetPosition() - transform.position;
            if (vecToGhost.sqrMagnitude <= 0.1f)
            {
                runningAway = true;
                //CalculatePath
                GraphNode closestStart = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(transform.position);
                GraphNode closestGoal = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(transform.position + vecToGhost.normalized * -0.6f+Vector3.right*Random.Range(-0.1f, 0.1f) + Vector3.down * Random.Range(-0.1f, 0.1f));
                path = HW3NavigationHandler.Instance.PathFinder.CalculatePath(closestStart, closestGoal);
                if (path == null || path.Length < 1)
                {
                    // Do nothing.
                }
                else
                {
                    pathIndex = 0;
                    SetTarget(path[pathIndex]);
                }
            }
        }

        if (!runningAway)
        {
            Pellet p = PelletHandler.Instance.GetClosestPellet(transform.position);
            if (p != null)
            {
                Vector3 target = p.transform.position;

                //CalculatePath	
                GraphNode closestStart = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(transform.position);
                GraphNode closestGoal = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(target);
                path = HW3NavigationHandler.Instance.PathFinder.CalculatePath(closestStart, closestGoal);

                if (path == null || path.Length < 1)
                {
                    // Do nothing.
                }
                else
                {
                    pathIndex = 0;
                    // The target should be the next node in the path only if we've reached the center of the current node.
                    Vector3 distBetweenTarget = transform.position - currTarget;
                    if (Mathf.Abs(distBetweenTarget.x) < 0.0001f && Mathf.Abs(distBetweenTarget.y) < 0.0001f)
                    {
                        currTarget = path[pathIndex];
                    }
                    SetTarget(currTarget);
                }
            }
            else
            {
                movingTowardTarget = false;
            }

        }


        if (movingTowardTarget)
        {
            if ((target - transform.position).sqrMagnitude < AgentConstants.THRESHOLD)
            {
                movingTowardTarget = false;
                transform.position = target;
            }
            else
            {
                Vector3 potentialNewPosition = transform.position + (target - transform.position).normalized * Time.deltaTime * speed;
                if (ObstacleHandler.Instance.AnyIntersect(new Vector2(transform.position.x, transform.position.y), new Vector2(potentialNewPosition.x, potentialNewPosition.y)))
                {
                    movingTowardTarget = false;
                }
                else
                {
                    transform.position = potentialNewPosition;
                }
            }

        }

    }
}
