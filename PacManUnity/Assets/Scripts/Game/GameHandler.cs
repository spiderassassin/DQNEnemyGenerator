using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler: MonoBehaviour
{
    public PelletHandler pelletHandler;

    void Start()
    {

        HW3NavigationHandler.Instance.Init();

        List<GraphNode> corners = new List<GraphNode>();

        float[] XBounds = ObstacleHandler.Instance.GetPathXBounds();
        float[] YBounds = ObstacleHandler.Instance.GetPathYBounds();
        foreach (float x in XBounds)
        {
            foreach (float y in YBounds)
            {
                GraphNode g = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(new Vector3(x, y));
                corners.Add(g);
            }
        }

        // Add pellets along the path.
        Vector2[][] path = ObstacleHandler.Instance.GetWalkablePath();
        foreach (Vector2[] line in path)
        {
            Vector3 pos = new Vector3(line[0].x, line[0].y, 0);
            GraphNode g = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(pos);
            if (corners.Contains(g))
            {
                pelletHandler.AddPellet(g.Location, true);
            }
            else
            {
                pelletHandler.AddPellet(g.Location);
            }
        }
    }
}
