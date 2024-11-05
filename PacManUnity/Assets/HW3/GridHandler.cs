using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

//Assignment 1 Part A Script
public class GridHandler : NodeHandler
{
    // Distance between nodes.
    public static float gridWidth = 28;
    public static float gridHeight = 32;
    public static float gridInterval = 0.12f;
    public static float inset = 0.06f;
    public float gridStartX = (gridWidth / 2 * -gridInterval);
    public float gridEndX = (gridWidth / 2 * gridInterval);
    public float gridStartY = (gridHeight / 2 * -gridInterval);
    public float gridEndY = (gridHeight / 2 * gridInterval);

    //Holds all of the nodes
    private Dictionary<string, GraphNode> nodeDictionary;
    public override void CreateNodes()
    {
        nodeDictionary = new Dictionary<string, GraphNode>();
        string activeScene = SceneManager.GetActiveScene().name;
        
        if (activeScene.Equals("Scene1"))
        {
            // Generate nodes for the grid.
            for (float x = gridStartX; x <= gridEndX; x += gridInterval)
            {
                for (float y = gridStartY; y <= gridEndY; y += gridInterval)
                {
                    Vector3 position = new Vector3(x, y, 0);
                    GraphNode node = new GraphNode(position);
                    nodeDictionary.Add(position.ToString(), node);
                }
            }
            // What is this monstrosity?
            // nodeDictionary.Add("(-1.8, -2.1, 0.0)", new GraphNode(new Vector3(-1.8f, -2.1f, 0f)));
            // nodeDictionary.Add("(-1.8, -1.9, 0.0)", new GraphNode(new Vector3(-1.8f, -1.9f, 0f)));
            // nodeDictionary.Add("(-1.8, -1.7, 0.0)", new GraphNode(new Vector3(-1.8f, -1.7f, 0f)));
            // nodeDictionary.Add("(-1.8, -1.5, 0.0)", new GraphNode(new Vector3(-1.8f, -1.5f, 0f)));
            // nodeDictionary.Add("(-1.8, -1.1, 0.0)", new GraphNode(new Vector3(-1.8f, -1.1f, 0f)));
            // nodeDictionary.Add("(-1.8, -0.9, 0.0)", new GraphNode(new Vector3(-1.8f, -0.8999997f, 0f)));
            // nodeDictionary.Add("(-1.8, -0.7, 0.0)", new GraphNode(new Vector3(-1.8f, -0.6999997f, 0f)));
            // nodeDictionary.Add("(-1.8, -0.5, 0.0)", new GraphNode(new Vector3(-1.8f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(-1.8, 0.1, 0.0)", new GraphNode(new Vector3(-1.8f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(-1.8, 0.9, 0.0)", new GraphNode(new Vector3(-1.8f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(-1.8, 1.1, 0.0)", new GraphNode(new Vector3(-1.8f, 1.1f, 0f)));
            // nodeDictionary.Add("(-1.8, 1.3, 0.0)", new GraphNode(new Vector3(-1.8f, 1.3f, 0f)));
            // nodeDictionary.Add("(-1.8, 1.5, 0.0)", new GraphNode(new Vector3(-1.8f, 1.5f, 0f)));
            // nodeDictionary.Add("(-1.8, 1.7, 0.0)", new GraphNode(new Vector3(-1.8f, 1.7f, 0f)));
            // nodeDictionary.Add("(-1.8, 1.9, 0.0)", new GraphNode(new Vector3(-1.8f, 1.9f, 0f)));
            // nodeDictionary.Add("(-1.8, 2.1, 0.0)", new GraphNode(new Vector3(-1.8f, 2.1f, 0f)));
            // nodeDictionary.Add("(-1.6, -2.1, 0.0)", new GraphNode(new Vector3(-1.6f, -2.1f, 0f)));
            // nodeDictionary.Add("(-1.6, -1.5, 0.0)", new GraphNode(new Vector3(-1.6f, -1.5f, 0f)));
            // nodeDictionary.Add("(-1.6, -1.3, 0.0)", new GraphNode(new Vector3(-1.6f, -1.3f, 0f)));
            // nodeDictionary.Add("(-1.6, -1.1, 0.0)", new GraphNode(new Vector3(-1.6f, -1.1f, 0f)));
            // nodeDictionary.Add("(-1.6, -0.5, 0.0)", new GraphNode(new Vector3(-1.6f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(-1.6, 0.1, 0.0)", new GraphNode(new Vector3(-1.6f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(-1.6, 0.9, 0.0)", new GraphNode(new Vector3(-1.6f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(-1.6, 1.5, 0.0)", new GraphNode(new Vector3(-1.6f, 1.5f, 0f)));
            // nodeDictionary.Add("(-1.6, 2.1, 0.0)", new GraphNode(new Vector3(-1.6f, 2.1f, 0f)));
            // nodeDictionary.Add("(-1.4, -2.1, 0.0)", new GraphNode(new Vector3(-1.4f, -2.1f, 0f)));
            // nodeDictionary.Add("(-1.4, -1.5, 0.0)", new GraphNode(new Vector3(-1.4f, -1.5f, 0f)));
            // nodeDictionary.Add("(-1.4, -0.5, 0.0)", new GraphNode(new Vector3(-1.4f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(-1.4, 0.1, 0.0)", new GraphNode(new Vector3(-1.4f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(-1.4, 0.9, 0.0)", new GraphNode(new Vector3(-1.4f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(-1.4, 1.5, 0.0)", new GraphNode(new Vector3(-1.4f, 1.5f, 0f)));
            // nodeDictionary.Add("(-1.4, 2.1, 0.0)", new GraphNode(new Vector3(-1.4f, 2.1f, 0f)));
            // nodeDictionary.Add("(-1.2, -2.1, 0.0)", new GraphNode(new Vector3(-1.2f, -2.1f, 0f)));
            // nodeDictionary.Add("(-1.2, -1.5, 0.0)", new GraphNode(new Vector3(-1.2f, -1.5f, 0f)));
            // nodeDictionary.Add("(-1.2, -0.5, 0.0)", new GraphNode(new Vector3(-1.2f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(-1.2, 0.1, 0.0)", new GraphNode(new Vector3(-1.2f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(-1.2, 0.9, 0.0)", new GraphNode(new Vector3(-1.2f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(-1.2, 1.5, 0.0)", new GraphNode(new Vector3(-1.2f, 1.5f, 0f)));
            // nodeDictionary.Add("(-1.2, 2.1, 0.0)", new GraphNode(new Vector3(-1.2f, 2.1f, 0f)));
            // nodeDictionary.Add("(-1.0, -2.1, 0.0)", new GraphNode(new Vector3(-0.9999998f, -2.1f, 0f)));
            // nodeDictionary.Add("(-1.0, -1.5, 0.0)", new GraphNode(new Vector3(-0.9999998f, -1.5f, 0f)));
            // nodeDictionary.Add("(-1.0, -1.3, 0.0)", new GraphNode(new Vector3(-0.9999998f, -1.3f, 0f)));
            // nodeDictionary.Add("(-1.0, -1.1, 0.0)", new GraphNode(new Vector3(-0.9999998f, -1.1f, 0f)));
            // nodeDictionary.Add("(-1.0, -0.9, 0.0)", new GraphNode(new Vector3(-0.9999998f, -0.8999997f, 0f)));
            // nodeDictionary.Add("(-1.0, -0.7, 0.0)", new GraphNode(new Vector3(-0.9999998f, -0.6999997f, 0f)));
            // nodeDictionary.Add("(-1.0, -0.5, 0.0)", new GraphNode(new Vector3(-0.9999998f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(-1.0, -0.3, 0.0)", new GraphNode(new Vector3(-0.9999998f, -0.2999997f, 0f)));
            // nodeDictionary.Add("(-1.0, -0.1, 0.0)", new GraphNode(new Vector3(-0.9999998f, -0.09999971f, 0f)));
            // nodeDictionary.Add("(-1.0, 0.1, 0.0)", new GraphNode(new Vector3(-0.9999998f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(-1.0, 0.3, 0.0)", new GraphNode(new Vector3(-0.9999998f, 0.3000003f, 0f)));
            // nodeDictionary.Add("(-1.0, 0.5, 0.0)", new GraphNode(new Vector3(-0.9999998f, 0.5000003f, 0f)));
            // nodeDictionary.Add("(-1.0, 0.7, 0.0)", new GraphNode(new Vector3(-0.9999998f, 0.7000003f, 0f)));
            // nodeDictionary.Add("(-1.0, 0.9, 0.0)", new GraphNode(new Vector3(-0.9999998f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(-1.0, 1.1, 0.0)", new GraphNode(new Vector3(-0.9999998f, 1.1f, 0f)));
            // nodeDictionary.Add("(-1.0, 1.3, 0.0)", new GraphNode(new Vector3(-0.9999998f, 1.3f, 0f)));
            // nodeDictionary.Add("(-1.0, 1.5, 0.0)", new GraphNode(new Vector3(-0.9999998f, 1.5f, 0f)));
            // nodeDictionary.Add("(-1.0, 1.7, 0.0)", new GraphNode(new Vector3(-0.9999998f, 1.7f, 0f)));
            // nodeDictionary.Add("(-1.0, 1.9, 0.0)", new GraphNode(new Vector3(-0.9999998f, 1.9f, 0f)));
            // nodeDictionary.Add("(-1.0, 2.1, 0.0)", new GraphNode(new Vector3(-0.9999998f, 2.1f, 0f)));
            // nodeDictionary.Add("(-0.8, -2.1, 0.0)", new GraphNode(new Vector3(-0.7999998f, -2.1f, 0f)));
            // nodeDictionary.Add("(-0.8, -1.1, 0.0)", new GraphNode(new Vector3(-0.7999998f, -1.1f, 0f)));
            // nodeDictionary.Add("(-0.8, -0.5, 0.0)", new GraphNode(new Vector3(-0.7999998f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(-0.8, 0.1, 0.0)", new GraphNode(new Vector3(-0.7999998f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(-0.8, 1.5, 0.0)", new GraphNode(new Vector3(-0.7999998f, 1.5f, 0f)));
            // nodeDictionary.Add("(-0.8, 2.1, 0.0)", new GraphNode(new Vector3(-0.7999998f, 2.1f, 0f)));
            // nodeDictionary.Add("(-0.6, -2.1, 0.0)", new GraphNode(new Vector3(-0.5999998f, -2.1f, 0f)));
            // nodeDictionary.Add("(-0.6, -1.1, 0.0)", new GraphNode(new Vector3(-0.5999998f, -1.1f, 0f)));
            // nodeDictionary.Add("(-0.6, -0.5, 0.0)", new GraphNode(new Vector3(-0.5999998f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(-0.6, 0.1, 0.0)", new GraphNode(new Vector3(-0.5999998f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(-0.6, 1.5, 0.0)", new GraphNode(new Vector3(-0.5999998f, 1.5f, 0f)));
            // nodeDictionary.Add("(-0.6, 2.1, 0.0)", new GraphNode(new Vector3(-0.5999998f, 2.1f, 0f)));
            // nodeDictionary.Add("(-0.4, -2.1, 0.0)", new GraphNode(new Vector3(-0.3999999f, -2.1f, 0f)));
            // nodeDictionary.Add("(-0.4, -1.5, 0.0)", new GraphNode(new Vector3(-0.3999999f, -1.5f, 0f)));
            // nodeDictionary.Add("(-0.4, -1.3, 0.0)", new GraphNode(new Vector3(-0.3999999f, -1.3f, 0f)));
            // nodeDictionary.Add("(-0.4, -1.1, 0.0)", new GraphNode(new Vector3(-0.3999999f, -1.1f, 0f)));
            // nodeDictionary.Add("(-0.4, -0.5, 0.0)", new GraphNode(new Vector3(-0.3999999f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(-0.4, -0.3, 0.0)", new GraphNode(new Vector3(-0.3999999f, -0.2999997f, 0f)));
            // nodeDictionary.Add("(-0.4, -0.1, 0.0)", new GraphNode(new Vector3(-0.3999999f, -0.09999971f, 0f)));
            // nodeDictionary.Add("(-0.4, 0.1, 0.0)", new GraphNode(new Vector3(-0.3999999f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(-0.4, 0.3, 0.0)", new GraphNode(new Vector3(-0.3999999f, 0.3000003f, 0f)));
            // nodeDictionary.Add("(-0.4, 0.5, 0.0)", new GraphNode(new Vector3(-0.3999999f, 0.5000003f, 0f)));
            // nodeDictionary.Add("(-0.4, 1.1, 0.0)", new GraphNode(new Vector3(-0.3999999f, 1.1f, 0f)));
            // nodeDictionary.Add("(-0.4, 1.3, 0.0)", new GraphNode(new Vector3(-0.3999999f, 1.3f, 0f)));
            // nodeDictionary.Add("(-0.4, 1.5, 0.0)", new GraphNode(new Vector3(-0.3999999f, 1.5f, 0f)));
            // nodeDictionary.Add("(-0.4, 2.1, 0.0)", new GraphNode(new Vector3(-0.3999999f, 2.1f, 0f)));
            // nodeDictionary.Add("(-0.2, -2.1, 0.0)", new GraphNode(new Vector3(-0.1999999f, -2.1f, 0f)));
            // nodeDictionary.Add("(-0.2, -1.9, 0.0)", new GraphNode(new Vector3(-0.1999999f, -1.9f, 0f)));
            // nodeDictionary.Add("(-0.2, -1.7, 0.0)", new GraphNode(new Vector3(-0.1999999f, -1.7f, 0f)));
            // nodeDictionary.Add("(-0.2, -1.5, 0.0)", new GraphNode(new Vector3(-0.1999999f, -1.5f, 0f)));
            // nodeDictionary.Add("(-0.2, -1.1, 0.0)", new GraphNode(new Vector3(-0.1999999f, -1.1f, 0f)));
            // nodeDictionary.Add("(-0.2, -0.9, 0.0)", new GraphNode(new Vector3(-0.1999999f, -0.8999997f, 0f)));
            // nodeDictionary.Add("(-0.2, -0.7, 0.0)", new GraphNode(new Vector3(-0.1999999f, -0.6999997f, 0f)));
            // nodeDictionary.Add("(-0.2, -0.5, 0.0)", new GraphNode(new Vector3(-0.1999999f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(-0.2, -0.1, 0.0)", new GraphNode(new Vector3(-0.1999999f, -0.09999971f, 0f)));
            // nodeDictionary.Add("(-0.2, 0.5, 0.0)", new GraphNode(new Vector3(-0.1999999f, 0.5000003f, 0f)));
            // nodeDictionary.Add("(-0.2, 0.7, 0.0)", new GraphNode(new Vector3(-0.1999999f, 0.7000003f, 0f)));
            // nodeDictionary.Add("(-0.2, 0.9, 0.0)", new GraphNode(new Vector3(-0.1999999f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(-0.2, 1.1, 0.0)", new GraphNode(new Vector3(-0.1999999f, 1.1f, 0f)));
            // nodeDictionary.Add("(-0.2, 1.5, 0.0)", new GraphNode(new Vector3(-0.1999999f, 1.5f, 0f)));
            // nodeDictionary.Add("(-0.2, 2.1, 0.0)", new GraphNode(new Vector3(-0.1999999f, 2.1f, 0f)));
            // nodeDictionary.Add("(0.0, -2.1, 0.0)", new GraphNode(new Vector3(1.490116E-07f, -2.1f, 0f)));
            // nodeDictionary.Add("(0.0, -1.1, 0.0)", new GraphNode(new Vector3(1.490116E-07f, -1.1f, 0f)));
            // nodeDictionary.Add("(0.0, -0.1, 0.0)", new GraphNode(new Vector3(1.490116E-07f, -0.09999971f, 0f)));
            // nodeDictionary.Add("(0.0, 0.5, 0.0)", new GraphNode(new Vector3(1.490116E-07f, 0.5000003f, 0f)));
            // nodeDictionary.Add("(0.0, 1.5, 0.0)", new GraphNode(new Vector3(1.490116E-07f, 1.5f, 0f)));
            // nodeDictionary.Add("(0.0, 1.7, 0.0)", new GraphNode(new Vector3(1.490116E-07f, 1.7f, 0f)));
            // nodeDictionary.Add("(0.0, 1.9, 0.0)", new GraphNode(new Vector3(1.490116E-07f, 1.9f, 0f)));
            // nodeDictionary.Add("(0.0, 2.1, 0.0)", new GraphNode(new Vector3(1.490116E-07f, 2.1f, 0f)));
            // nodeDictionary.Add("(0.2, -2.1, 0.0)", new GraphNode(new Vector3(0.2000002f, -2.1f, 0f)));
            // nodeDictionary.Add("(0.2, -1.9, 0.0)", new GraphNode(new Vector3(0.2000002f, -1.9f, 0f)));
            // nodeDictionary.Add("(0.2, -1.7, 0.0)", new GraphNode(new Vector3(0.2000002f, -1.7f, 0f)));
            // nodeDictionary.Add("(0.2, -1.5, 0.0)", new GraphNode(new Vector3(0.2000002f, -1.5f, 0f)));
            // nodeDictionary.Add("(0.2, -1.1, 0.0)", new GraphNode(new Vector3(0.2000002f, -1.1f, 0f)));
            // nodeDictionary.Add("(0.2, -0.9, 0.0)", new GraphNode(new Vector3(0.2000002f, -0.8999997f, 0f)));
            // nodeDictionary.Add("(0.2, -0.7, 0.0)", new GraphNode(new Vector3(0.2000002f, -0.6999997f, 0f)));
            // nodeDictionary.Add("(0.2, -0.5, 0.0)", new GraphNode(new Vector3(0.2000002f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(0.2, -0.1, 0.0)", new GraphNode(new Vector3(0.2000002f, -0.09999971f, 0f)));
            // nodeDictionary.Add("(0.2, 0.5, 0.0)", new GraphNode(new Vector3(0.2000002f, 0.5000003f, 0f)));
            // nodeDictionary.Add("(0.2, 0.7, 0.0)", new GraphNode(new Vector3(0.2000002f, 0.7000003f, 0f)));
            // nodeDictionary.Add("(0.2, 0.9, 0.0)", new GraphNode(new Vector3(0.2000002f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(0.2, 1.1, 0.0)", new GraphNode(new Vector3(0.2000002f, 1.1f, 0f)));
            // nodeDictionary.Add("(0.2, 1.5, 0.0)", new GraphNode(new Vector3(0.2000002f, 1.5f, 0f)));
            // nodeDictionary.Add("(0.2, 2.1, 0.0)", new GraphNode(new Vector3(0.2000002f, 2.1f, 0f)));
            // nodeDictionary.Add("(0.4, -2.1, 0.0)", new GraphNode(new Vector3(0.4000002f, -2.1f, 0f)));
            // nodeDictionary.Add("(0.4, -1.5, 0.0)", new GraphNode(new Vector3(0.4000002f, -1.5f, 0f)));
            // nodeDictionary.Add("(0.4, -1.3, 0.0)", new GraphNode(new Vector3(0.4000002f, -1.3f, 0f)));
            // nodeDictionary.Add("(0.4, -1.1, 0.0)", new GraphNode(new Vector3(0.4000002f, -1.1f, 0f)));
            // nodeDictionary.Add("(0.4, -0.5, 0.0)", new GraphNode(new Vector3(0.4000002f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(0.4, -0.3, 0.0)", new GraphNode(new Vector3(0.4000002f, -0.2999997f, 0f)));
            // nodeDictionary.Add("(0.4, -0.1, 0.0)", new GraphNode(new Vector3(0.4000002f, -0.09999971f, 0f)));
            // nodeDictionary.Add("(0.4, 0.1, 0.0)", new GraphNode(new Vector3(0.4000002f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(0.4, 0.3, 0.0)", new GraphNode(new Vector3(0.4000002f, 0.3000003f, 0f)));
            // nodeDictionary.Add("(0.4, 0.5, 0.0)", new GraphNode(new Vector3(0.4000002f, 0.5000003f, 0f)));
            // nodeDictionary.Add("(0.4, 1.1, 0.0)", new GraphNode(new Vector3(0.4000002f, 1.1f, 0f)));
            // nodeDictionary.Add("(0.4, 1.3, 0.0)", new GraphNode(new Vector3(0.4000002f, 1.3f, 0f)));
            // nodeDictionary.Add("(0.4, 1.5, 0.0)", new GraphNode(new Vector3(0.4000002f, 1.5f, 0f)));
            // nodeDictionary.Add("(0.4, 2.1, 0.0)", new GraphNode(new Vector3(0.4000002f, 2.1f, 0f)));
            // nodeDictionary.Add("(0.6, -2.1, 0.0)", new GraphNode(new Vector3(0.6000001f, -2.1f, 0f)));
            // nodeDictionary.Add("(0.6, -1.1, 0.0)", new GraphNode(new Vector3(0.6000001f, -1.1f, 0f)));
            // nodeDictionary.Add("(0.6, -0.5, 0.0)", new GraphNode(new Vector3(0.6000001f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(0.6, 0.1, 0.0)", new GraphNode(new Vector3(0.6000001f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(0.6, 1.5, 0.0)", new GraphNode(new Vector3(0.6000001f, 1.5f, 0f)));
            // nodeDictionary.Add("(0.6, 2.1, 0.0)", new GraphNode(new Vector3(0.6000001f, 2.1f, 0f)));
            // nodeDictionary.Add("(0.8, -2.1, 0.0)", new GraphNode(new Vector3(0.8000001f, -2.1f, 0f)));
            // nodeDictionary.Add("(0.8, -1.1, 0.0)", new GraphNode(new Vector3(0.8000001f, -1.1f, 0f)));
            // nodeDictionary.Add("(0.8, -0.5, 0.0)", new GraphNode(new Vector3(0.8000001f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(0.8, 0.1, 0.0)", new GraphNode(new Vector3(0.8000001f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(0.8, 1.5, 0.0)", new GraphNode(new Vector3(0.8000001f, 1.5f, 0f)));
            // nodeDictionary.Add("(0.8, 2.1, 0.0)", new GraphNode(new Vector3(0.8000001f, 2.1f, 0f)));
            // nodeDictionary.Add("(1.0, -2.1, 0.0)", new GraphNode(new Vector3(1f, -2.1f, 0f)));
            // nodeDictionary.Add("(1.0, -1.5, 0.0)", new GraphNode(new Vector3(1f, -1.5f, 0f)));
            // nodeDictionary.Add("(1.0, -1.3, 0.0)", new GraphNode(new Vector3(1f, -1.3f, 0f)));
            // nodeDictionary.Add("(1.0, -1.1, 0.0)", new GraphNode(new Vector3(1f, -1.1f, 0f)));
            // nodeDictionary.Add("(1.0, -0.9, 0.0)", new GraphNode(new Vector3(1f, -0.8999997f, 0f)));
            // nodeDictionary.Add("(1.0, -0.7, 0.0)", new GraphNode(new Vector3(1f, -0.6999997f, 0f)));
            // nodeDictionary.Add("(1.0, -0.5, 0.0)", new GraphNode(new Vector3(1f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(1.0, -0.3, 0.0)", new GraphNode(new Vector3(1f, -0.2999997f, 0f)));
            // nodeDictionary.Add("(1.0, -0.1, 0.0)", new GraphNode(new Vector3(1f, -0.09999971f, 0f)));
            // nodeDictionary.Add("(1.0, 0.1, 0.0)", new GraphNode(new Vector3(1f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(1.0, 0.3, 0.0)", new GraphNode(new Vector3(1f, 0.3000003f, 0f)));
            // nodeDictionary.Add("(1.0, 0.5, 0.0)", new GraphNode(new Vector3(1f, 0.5000003f, 0f)));
            // nodeDictionary.Add("(1.0, 0.7, 0.0)", new GraphNode(new Vector3(1f, 0.7000003f, 0f)));
            // nodeDictionary.Add("(1.0, 0.9, 0.0)", new GraphNode(new Vector3(1f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(1.0, 1.1, 0.0)", new GraphNode(new Vector3(1f, 1.1f, 0f)));
            // nodeDictionary.Add("(1.0, 1.3, 0.0)", new GraphNode(new Vector3(1f, 1.3f, 0f)));
            // nodeDictionary.Add("(1.0, 1.5, 0.0)", new GraphNode(new Vector3(1f, 1.5f, 0f)));
            // nodeDictionary.Add("(1.0, 1.7, 0.0)", new GraphNode(new Vector3(1f, 1.7f, 0f)));
            // nodeDictionary.Add("(1.0, 1.9, 0.0)", new GraphNode(new Vector3(1f, 1.9f, 0f)));
            // nodeDictionary.Add("(1.0, 2.1, 0.0)", new GraphNode(new Vector3(1f, 2.1f, 0f)));
            // nodeDictionary.Add("(1.2, -2.1, 0.0)", new GraphNode(new Vector3(1.2f, -2.1f, 0f)));
            // nodeDictionary.Add("(1.2, -1.5, 0.0)", new GraphNode(new Vector3(1.2f, -1.5f, 0f)));
            // nodeDictionary.Add("(1.2, -0.5, 0.0)", new GraphNode(new Vector3(1.2f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(1.2, 0.1, 0.0)", new GraphNode(new Vector3(1.2f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(1.2, 0.9, 0.0)", new GraphNode(new Vector3(1.2f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(1.2, 1.5, 0.0)", new GraphNode(new Vector3(1.2f, 1.5f, 0f)));
            // nodeDictionary.Add("(1.2, 2.1, 0.0)", new GraphNode(new Vector3(1.2f, 2.1f, 0f)));
            // nodeDictionary.Add("(1.4, -2.1, 0.0)", new GraphNode(new Vector3(1.4f, -2.1f, 0f)));
            // nodeDictionary.Add("(1.4, -1.5, 0.0)", new GraphNode(new Vector3(1.4f, -1.5f, 0f)));
            // nodeDictionary.Add("(1.4, -0.5, 0.0)", new GraphNode(new Vector3(1.4f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(1.4, 0.1, 0.0)", new GraphNode(new Vector3(1.4f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(1.4, 0.9, 0.0)", new GraphNode(new Vector3(1.4f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(1.4, 1.5, 0.0)", new GraphNode(new Vector3(1.4f, 1.5f, 0f)));
            // nodeDictionary.Add("(1.4, 2.1, 0.0)", new GraphNode(new Vector3(1.4f, 2.1f, 0f)));
            // nodeDictionary.Add("(1.6, -2.1, 0.0)", new GraphNode(new Vector3(1.6f, -2.1f, 0f)));
            // nodeDictionary.Add("(1.6, -1.5, 0.0)", new GraphNode(new Vector3(1.6f, -1.5f, 0f)));
            // nodeDictionary.Add("(1.6, -1.3, 0.0)", new GraphNode(new Vector3(1.6f, -1.3f, 0f)));
            // nodeDictionary.Add("(1.6, -1.1, 0.0)", new GraphNode(new Vector3(1.6f, -1.1f, 0f)));
            // nodeDictionary.Add("(1.6, -0.5, 0.0)", new GraphNode(new Vector3(1.6f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(1.6, 0.1, 0.0)", new GraphNode(new Vector3(1.6f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(1.6, 0.9, 0.0)", new GraphNode(new Vector3(1.6f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(1.6, 1.5, 0.0)", new GraphNode(new Vector3(1.6f, 1.5f, 0f)));
            // nodeDictionary.Add("(1.6, 2.1, 0.0)", new GraphNode(new Vector3(1.6f, 2.1f, 0f)));
            // nodeDictionary.Add("(1.8, -2.1, 0.0)", new GraphNode(new Vector3(1.8f, -2.1f, 0f)));
            // nodeDictionary.Add("(1.8, -1.9, 0.0)", new GraphNode(new Vector3(1.8f, -1.9f, 0f)));
            // nodeDictionary.Add("(1.8, -1.7, 0.0)", new GraphNode(new Vector3(1.8f, -1.7f, 0f)));
            // nodeDictionary.Add("(1.8, -1.5, 0.0)", new GraphNode(new Vector3(1.8f, -1.5f, 0f)));
            // nodeDictionary.Add("(1.8, -1.1, 0.0)", new GraphNode(new Vector3(1.8f, -1.1f, 0f)));
            // nodeDictionary.Add("(1.8, -0.9, 0.0)", new GraphNode(new Vector3(1.8f, -0.8999997f, 0f)));
            // nodeDictionary.Add("(1.8, -0.7, 0.0)", new GraphNode(new Vector3(1.8f, -0.6999997f, 0f)));
            // nodeDictionary.Add("(1.8, -0.5, 0.0)", new GraphNode(new Vector3(1.8f, -0.4999997f, 0f)));
            // nodeDictionary.Add("(1.8, 0.1, 0.0)", new GraphNode(new Vector3(1.8f, 0.1000003f, 0f)));
            // nodeDictionary.Add("(1.8, 0.9, 0.0)", new GraphNode(new Vector3(1.8f, 0.9000003f, 0f)));
            // nodeDictionary.Add("(1.8, 1.1, 0.0)", new GraphNode(new Vector3(1.8f, 1.1f, 0f)));
            // nodeDictionary.Add("(1.8, 1.3, 0.0)", new GraphNode(new Vector3(1.8f, 1.3f, 0f)));
            // nodeDictionary.Add("(1.8, 1.5, 0.0)", new GraphNode(new Vector3(1.8f, 1.5f, 0f)));
            // nodeDictionary.Add("(1.8, 1.7, 0.0)", new GraphNode(new Vector3(1.8f, 1.7f, 0f)));
            // nodeDictionary.Add("(1.8, 1.9, 0.0)", new GraphNode(new Vector3(1.8f, 1.9f, 0f)));
            // nodeDictionary.Add("(1.8, 2.1, 0.0)", new GraphNode(new Vector3(1.8f, 2.1f, 0f)));
        }

        //Create Neighbors
        foreach (KeyValuePair<string, GraphNode> kvp in nodeDictionary)
        {
            //Left
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.left * gridInterval)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.left * gridInterval)).ToString()]);
            }
            //Right
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.right * gridInterval)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.right * gridInterval)).ToString()]);
            }
            //Up
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.up * gridInterval)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.up * gridInterval)).ToString()]);
            }
            //Down
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.down * gridInterval)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.down * gridInterval)).ToString()]);
            }
        }
    }

    public override void VisualizeNodes()
    {
        //Visualize grid points
        foreach (KeyValuePair<string, GraphNode> kvp in nodeDictionary)
        {
            //Draw left line
            Debug.DrawLine(kvp.Value.Location + Vector3.left * gridInterval / 2f + Vector3.up * gridInterval / 2f, kvp.Value.Location + Vector3.left * gridInterval / 2f + Vector3.down * gridInterval / 2f, Color.white);
            //Draw right line
            Debug.DrawLine(kvp.Value.Location + Vector3.right * gridInterval / 2f + Vector3.up * gridInterval / 2f, kvp.Value.Location + Vector3.right * gridInterval / 2f + Vector3.down * gridInterval / 2f, Color.white);
            //Draw top line
            Debug.DrawLine(kvp.Value.Location + Vector3.up * gridInterval / 2f + Vector3.left * gridInterval / 2f, kvp.Value.Location + Vector3.up * gridInterval / 2f + Vector3.right * gridInterval / 2f, Color.white);
            //Draw bottom line
            Debug.DrawLine(kvp.Value.Location + Vector3.down * gridInterval / 2f + Vector3.left * gridInterval / 2f, kvp.Value.Location + Vector3.down * gridInterval / 2f + Vector3.right * gridInterval / 2f, Color.white);
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
