using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        string activeScene = SceneManager.GetActiveScene().name;
        
        if (activeScene.Equals("Scene1"))
        {
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

        //Create Neighbors
        foreach (KeyValuePair<string, GraphNode> kvp in nodeDictionary)
        {
            //Left
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.left * Config.GRID_INTERVAL)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.left * Config.GRID_INTERVAL)).ToString()]);
            }
            //Right
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.right * Config.GRID_INTERVAL)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.right * Config.GRID_INTERVAL)).ToString()]);
            }
            //Up
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.up * Config.GRID_INTERVAL)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.up * Config.GRID_INTERVAL)).ToString()]);
            }
            //Down
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.down * Config.GRID_INTERVAL)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.down * Config.GRID_INTERVAL)).ToString()]);
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
}
