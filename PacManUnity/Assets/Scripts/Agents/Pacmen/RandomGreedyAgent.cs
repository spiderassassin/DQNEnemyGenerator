using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGreedyAgent : MonoBehaviour
{
    private float speed = AgentConstants.SPEED;
    private Vector3 target;
    private bool movingTowardTarget = true;
    public bool MovingTowardTarget { get { return movingTowardTarget; } }

    private const int RIGHT = 0, DOWN = 1, LEFT = 2, UP = 3;
    private int currDirection = 0;  //0 = right, 1 = down, 2 = left, 3 = up
    private int nextDirection = 0;

    private float randomTimer = 0;
    private const float RANDOM_TIMER_MAX = 0.5f;

    //Path
    private Vector3[] path;
    private int pathIndex;
    private Vector3 currTarget;

    [SerializeField] private HW3NavigationHandler hw3NavigationHandler;
    [SerializeField] private PelletHandler pelletHandler;
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
    }

    void Start()
    {
        Vector3 currPos = transform.localPosition;
        currPos += new Vector3(0.001f, 0, 0);
        SetTarget(currPos);
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0.0f, 1.0f) > 0.5f){
            // Greedy
            Pellet p = pelletHandler.GetClosestPellet(transform.localPosition);
            if (p != null)
            {
                Vector3 target = p.transform.localPosition;

                //CalculatePath	
                GraphNode closestStart = hw3NavigationHandler.NodeHandler.ClosestNode(transform.localPosition);
                GraphNode closestGoal = hw3NavigationHandler.NodeHandler.ClosestNode(target);
                path = hw3NavigationHandler.PathFinder.CalculatePath(closestStart, closestGoal);

                if (path == null || path.Length < 1)
                {
                    // Do nothing.
                }
                else
                {
                    pathIndex = 0;
                    // The target should be the next node in the path only if we've reached the center of the current node.
                    Vector3 distBetweenTarget = transform.localPosition - currTarget;
                    if (distBetweenTarget.x < 0.0001f && distBetweenTarget.y < 0.0001f)
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

        else{
            //random
            if (randomTimer < RANDOM_TIMER_MAX)
            {
                randomTimer += Time.deltaTime;
            }
            else
            {
                randomTimer = 0;
                nextDirection = Random.RandomRange(0, 4);
            }

            Vector3 nextVectorDirection = ActionToDirection(nextDirection);
            Vector3 nextNode = transform.localPosition + nextVectorDirection * Config.AGENT_MOVE_INTERVAL;
            if (LegalAction(nextNode))
            {
                // Get corrected direction.
                Vector3 target = obstacleHandler.GetCorrectedTarget(nextVectorDirection, nextNode);
                SetTarget(target);
                currDirection = nextDirection;
            }
            else
            {
                // Keep going in the same direction.
                nextVectorDirection = ActionToDirection(currDirection);
                nextNode = transform.localPosition + nextVectorDirection * Config.AGENT_MOVE_INTERVAL;
                if (LegalAction(nextNode))
                {
                    // Get corrected direction.
                    Vector3 target = obstacleHandler.GetCorrectedTarget(nextVectorDirection, nextNode);
                    SetTarget(target);
                }
                else
                {
                    // Stay in the same place.
                    SetTarget(transform.localPosition);
                }
            }

            if ((target - transform.localPosition).sqrMagnitude < AgentConstants.THRESHOLD)
            {
                transform.localPosition = target;
            }
            else
            {
                Vector3 potentialNewPosition = transform.localPosition + (target - transform.localPosition).normalized * Time.deltaTime * speed;
                if (!obstacleHandler.AnyIntersect(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(potentialNewPosition.x, potentialNewPosition.y)))
                {
                    transform.localPosition = potentialNewPosition;
                }
            }
        }
    }

    private Vector3 ActionToDirection(int action)
    {
        switch (action)
        {
            case UP:
                return Vector3.up;
            case DOWN:
                return Vector3.down;
            case LEFT:
                return Vector3.left;
            case RIGHT:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }

    // Check if the next node in the direction of the action is on the path.
    private bool LegalAction(Vector3 nextNode)
    {
        return obstacleHandler.CheckPointOnPath(new Vector2(nextNode.x, nextNode.y), new Vector2(transform.localPosition.x, transform.localPosition.y));
    }
}
