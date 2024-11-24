using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

//Assignment 1 Part A Script
public class GridHandler : NodeHandler
{
    public float gridStartX = (Config.GRID_WIDTH / 2 * -Config.GRID_INTERVAL);
    public float gridEndX = (Config.GRID_WIDTH / 2 * Config.GRID_INTERVAL - Config.GRID_INTERVAL);
    public float gridStartY = (Config.GRID_HEIGHT / 2 * -Config.GRID_INTERVAL);
    public float gridEndY = (Config.GRID_HEIGHT / 2 * Config.GRID_INTERVAL - Config.GRID_INTERVAL);

    //Holds all of the nodes
    private Dictionary<string, GraphNode> nodeDictionary;
    public override void CreateNodes()
    {
        nodeDictionary = new Dictionary<string, GraphNode>();
        // Generate nodes for the grid.
        for (float x = gridStartX; x <= gridEndX; x += Config.GRID_INTERVAL)
        {
            for (float y = gridStartY; y <= gridEndY; y += Config.GRID_INTERVAL)
            {
                Vector3 position = new Vector3(x, y, 0);
                GraphNode node = new GraphNode(position);
                nodeDictionary.Add(position.ToString(), node);
            }
        }
    }

    public override void VisualizeNodes()
    {
        //Visualize grid points
        foreach (KeyValuePair<string, GraphNode> kvp in nodeDictionary)
        {
            //Draw left line
            Debug.DrawLine(kvp.Value.Location + Vector3.left * Config.GRID_INTERVAL / 2f + Vector3.up * Config.GRID_INTERVAL / 2f, kvp.Value.Location + Vector3.left * Config.GRID_INTERVAL / 2f + Vector3.down * Config.GRID_INTERVAL / 2f, Color.white);
            //Draw right line
            Debug.DrawLine(kvp.Value.Location + Vector3.right * Config.GRID_INTERVAL / 2f + Vector3.up * Config.GRID_INTERVAL / 2f, kvp.Value.Location + Vector3.right * Config.GRID_INTERVAL / 2f + Vector3.down * Config.GRID_INTERVAL / 2f, Color.white);
            //Draw top line
            Debug.DrawLine(kvp.Value.Location + Vector3.up * Config.GRID_INTERVAL / 2f + Vector3.left * Config.GRID_INTERVAL / 2f, kvp.Value.Location + Vector3.up * Config.GRID_INTERVAL / 2f + Vector3.right * Config.GRID_INTERVAL / 2f, Color.white);
            //Draw bottom line
            Debug.DrawLine(kvp.Value.Location + Vector3.down * Config.GRID_INTERVAL / 2f + Vector3.left * Config.GRID_INTERVAL / 2f, kvp.Value.Location + Vector3.down * Config.GRID_INTERVAL / 2f + Vector3.right * Config.GRID_INTERVAL / 2f, Color.white);
        }
    }

    //Find closest node (used for pathing)
    public override GraphNode ClosestNode(Vector3 position)
    {
        float minDist = 1000;
        GraphNode closest = null;
        foreach (KeyValuePair<string, GraphNode> kvp in nodeDictionary)
        {
            float dist = (kvp.Value.Location - position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                closest = kvp.Value;
            }
        }
        return closest;
    }

    // Used for pathfinding.
    public override void SetNeighbours(Vector2[][] path)
    {
        // Create list of direction vectors.
        Vector3[] directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.up, Vector3.down };
        foreach (KeyValuePair<string, GraphNode> kvp in nodeDictionary)
        {
            foreach (Vector3 direction in directions)
            {
                // Location of the neighbour node.
                Vector3 neighborLocation = kvp.Value.Location + direction * Config.GRID_INTERVAL;
                // Location of the path segment end, which stops at the edge of the current node, and the start of the neighbor node.
                Vector3 pathSegmentEnd = kvp.Value.Location + direction * Config.GRID_INTERVAL / 2;
                if (nodeDictionary.ContainsKey(neighborLocation.ToString()))
                {
                    // Check if it is in the path list.
                    Vector2[] lineSegment1 = new Vector2[] { kvp.Value.Location, pathSegmentEnd };
                    Vector2[] lineSegment2 = new Vector2[] { pathSegmentEnd, kvp.Value.Location };
                    if (path.Any(a => ArraysEqual(a, lineSegment1)) || path.Any(a => ArraysEqual(a, lineSegment2)))
                    {
                        kvp.Value.AddNeighbor(nodeDictionary[neighborLocation.ToString()]);
                    }
                }
            }
        }
    }

    private bool ArraysEqual(Vector2[] a1, Vector2[] a2, float tol = 0.0001f)
    {
        if (a1.Length != a2.Length)
            return false;

        for (int i = 0; i < a1.Length; i++)
        {
            if (Vector2.Distance(a1[i], a2[i]) > tol)
                return false;
        }

        return true;
    }
}
