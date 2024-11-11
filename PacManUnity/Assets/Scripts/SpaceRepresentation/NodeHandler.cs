using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHandler
{

	public virtual void CreateNodes()
	{
	}
	public virtual GraphNode ClosestNode(Vector3 position)
	{
		return null;
	}

    public virtual void VisualizeNodes()
    {

    }

	public virtual void SetNeighbours(Vector2[][] path) {}

}
