using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanInfo : MonoBehaviour
{
    public static PacmanInfo Instance;
    private Vector3 prevLocation;

    private Vector3 facing;
    public Vector3 Facing { get { return facing; } }

    private Vector3 startPosition;

    // Make a callback that we can subscribe to for when we reset.
    public delegate void ResetCallback();
    public event ResetCallback OnReset;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        startPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.y - prevLocation.y > AgentConstants.EPSILON)
        {
            facing = Vector3.up;
        }
        else if (transform.position.y - prevLocation.y < -1 * AgentConstants.EPSILON)
        {
            facing = Vector3.down;
        }
        else if (transform.position.x - prevLocation.x > AgentConstants.EPSILON)
        {
            facing = Vector3.right;
        }
        else
        {
            facing = Vector3.left;
        }

        prevLocation = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        prevLocation = startPosition;
        // Trigger a reset event.
        OnReset();
    }
}
