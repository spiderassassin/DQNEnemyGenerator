using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardAgent : MonoBehaviour
{
    private Vector3 target;
    private bool movingTowardTarget;
    public bool MovingTowardTarget { get { return movingTowardTarget; } }

    private float speed = AgentConstants.SPEED;

    //Path
    private Vector3[] path;
    private int pathIndex;
    private Vector3 currTarget;

    [SerializeField] private HW3NavigationHandler hw3NavigationHandler;
    [SerializeField] private GhostManager ghostManager;
    [SerializeField] private ObstacleHandler obstacleHandler;

    public void SetTarget(Vector3 _target)
    {
        target = _target;
        if (Mathf.Abs(transform.localPosition.x - target.x) < AgentConstants.EPSILON)//Special case moving vertically
        {
            Vector3 eulerAngles = transform.eulerAngles;
            if (transform.localPosition.y < target.y)
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
        Vector3 currPos = transform.localPosition;
        currPos += new Vector3(0.1f, 0, 0);
        SetTarget(currPos);
        currTarget = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        bool runningAway = true;
        FSMAgent ghost = ghostManager.GetClosestGhost(transform.localPosition);
        if (ghost != null)
        {
            Vector3 vecToGhost = ghost.GetPosition() - transform.localPosition;
            //runningAway = true;
            //CalculatePath
            GraphNode closestStart = hw3NavigationHandler.NodeHandler.ClosestNode(transform.localPosition);
            GraphNode closestGoal = hw3NavigationHandler.NodeHandler.ClosestNode(transform.localPosition + vecToGhost.normalized * -0.6f+Vector3.right*Random.Range(-0.1f, 0.1f) + Vector3.down * Random.Range(-0.1f, 0.1f));
            path = hw3NavigationHandler.PathFinder.CalculatePath(closestStart, closestGoal);
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


        if (movingTowardTarget)
        {
            if ((target - transform.localPosition).sqrMagnitude < AgentConstants.THRESHOLD)
            {
                movingTowardTarget = false;
                transform.localPosition = target;
            }
            else
            {
                Vector3 potentialNewPosition = transform.localPosition + (target - transform.localPosition).normalized * Time.deltaTime * speed;
                if (obstacleHandler.AnyIntersect(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(potentialNewPosition.x, potentialNewPosition.y)))
                {
                    movingTowardTarget = false;
                }
                else
                {
                    transform.localPosition = potentialNewPosition;
                }
            }

        }

    }
}