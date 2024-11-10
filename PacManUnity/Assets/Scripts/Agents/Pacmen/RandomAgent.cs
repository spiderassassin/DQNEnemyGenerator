using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAgent : MonoBehaviour
{
    private Vector3 target;
    private bool movingTowardTarget = true;
    public bool MovingTowardTarget { get { return movingTowardTarget; } }

    private const int RIGHT = 0, DOWN = 1, LEFT = 2, UP = 3;
    private int currDirection = 0;  //0 = right, 1 = down, 2 = left, 3 = up
    private int nextDirection = 0;

    private float randomTimer = 0;
    private const float RANDOM_TIMER_MAX = 0.5f;

    private float speed = AgentConstants.SPEED;

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
    }

    void Start()
    {
        Vector3 currPos = transform.position;
        currPos += new Vector3(0.001f, 0, 0);
        SetTarget(currPos);
    }

    // Update is called once per frame
    void Update()
    {
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
        Vector3 nextNode = transform.position + nextVectorDirection * Config.AGENT_MOVE_INTERVAL;
        if (LegalAction(nextNode))
        {
            // Get corrected direction.
            Vector3 target = ObstacleHandler.Instance.GetCorrectedTarget(nextVectorDirection, nextNode);
            SetTarget(target);
            currDirection = nextDirection;
        }
        else
        {
            // Keep going in the same direction.
            nextVectorDirection = ActionToDirection(currDirection);
            nextNode = transform.position + nextVectorDirection * Config.AGENT_MOVE_INTERVAL;
            if (LegalAction(nextNode))
            {
                // Get corrected direction.
                Vector3 target = ObstacleHandler.Instance.GetCorrectedTarget(nextVectorDirection, nextNode);
                SetTarget(target);
            }
            else
            {
                // Stay in the same place.
                SetTarget(transform.position);
            }
        }

        if ((target - transform.position).sqrMagnitude < AgentConstants.THRESHOLD)
        {
            transform.position = target;
        }
        else
        {
            Vector3 potentialNewPosition = transform.position + (target - transform.position).normalized * Time.deltaTime * speed;
            if (!ObstacleHandler.Instance.AnyIntersect(new Vector2(transform.position.x, transform.position.y), new Vector2(potentialNewPosition.x, potentialNewPosition.y)))
            {
                transform.position = potentialNewPosition;
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
        return ObstacleHandler.Instance.CheckPointOnPath(new Vector2(nextNode.x, nextNode.y), new Vector2(transform.position.x, transform.position.y));
    }
}
