using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanInfo : MonoBehaviour
{
    private Vector3 prevLocation;

    private Vector3 facing;
    public Vector3 Facing { get { return facing; } }

    // Start is called before the first frame update
    void Awake()
    {

    }

    void Update()
    {
        if (transform.localPosition.y - prevLocation.y > AgentConstants.EPSILON)
        {
            facing = Vector3.up;
        }
        else if (transform.localPosition.y - prevLocation.y < -1 * AgentConstants.EPSILON)
        {
            facing = Vector3.down;
        }
        else if (transform.localPosition.x - prevLocation.x > AgentConstants.EPSILON)
        {
            facing = Vector3.right;
        }
        else
        {
            facing = Vector3.left;
        }

        prevLocation = transform.localPosition;
    }
}
