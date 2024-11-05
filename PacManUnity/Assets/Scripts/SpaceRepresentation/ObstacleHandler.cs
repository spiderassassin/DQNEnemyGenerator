using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ObstacleHandler : MonoBehaviour
{
	public Material obstacleLineMaterial;

	public static ObstacleHandler Instance;
	private List<Polygon> obstacles = new List<Polygon>();
	public Polygon[] Obstacles{ get { return obstacles.ToArray(); } }
	private ObstacleDefiner obstacleDefiner;

	private Vector2[][] path;
    
	public float XBound { get { return obstacleDefiner.XBound; } }
	public float YBound { get { return obstacleDefiner.YBound; } }

	//Size of grid points
	private float gridSize = 0.2f;
	public float GridSize { get { return gridSize; } }

	//Initialize this singleton
	public void Init(ObstacleDefiner _obstacleDefiner)
	{
		ObstacleHandler.Instance = this;
		obstacleDefiner = _obstacleDefiner;
		path = obstacleDefiner.GetWalkablePath();
	}

	//Creates and renders an individual obstacle
	private void CreateAndRenderObstacle(Vector2[] points, string obstacleName = "")
	{
		GameObject newObstacleObj = new GameObject();
		if (obstacleName.Length > 0)
		{
			newObstacleObj.name = obstacleName;
		}
		Polygon obstacle = new Polygon(points, obstacleLineMaterial);//newObstacleObj.GetComponent<Polygon>();

		obstacles.Add(obstacle);

		obstacle.RenderPolygon(newObstacleObj);
	}

	//Creates and renders all obstacles
	public void CreateAndRenderObstacles(Vector2[][] arrayOfPoints)
	{
		for(int o=0; o<arrayOfPoints.Length; o++)
		{
			Vector2[] points = arrayOfPoints[o];
			CreateAndRenderObstacle(points, "Obstacle " + o);
		}
	}

    //Do any polygons intersect between these two  points?
	public bool AnyIntersect(Vector2 pt1, Vector2 pt2) {
		foreach (Polygon o in obstacles)
		{
			if (o.AnyIntersect(pt1, pt2)) {
				return true;
			}
		}
		return false;
	}

    //Purely so one doesn't have to convert vector3 to vector2
    public bool AnyIntersect(Vector3 pt1, Vector3 pt2)
    {
        return AnyIntersect(new Vector2(pt1.x, pt1.y), new Vector2(pt2.x, pt2.y));
    }

    //Returns true if this point is in any obstacle
    public bool PointInObstacles(Vector2 pnt)
	{
		foreach (Polygon obst in obstacles)
		{
			if (obst.ContainsPoint(pnt))
			{
				return true;
			}
		}
		return false;
	}

	public bool PointInObstacles(Vector3 pnt)
	{
		return PointInObstacles(new Vector2(pnt.x, pnt.y));
	}

    //Returns the corners of the map
    public Vector2[] GetMapCorners()
    {
        return new Vector2[] { new Vector2(ObstacleHandler.Instance.XBound * -1, ObstacleHandler.Instance.YBound * -1),
            new Vector2(ObstacleHandler.Instance.XBound * -1, ObstacleHandler.Instance.YBound),
        new Vector2(ObstacleHandler.Instance.XBound, ObstacleHandler.Instance.YBound),
        new Vector2(ObstacleHandler.Instance.XBound, ObstacleHandler.Instance.YBound * -1)};
    }

    //Returns an array of obstacle points
    public Vector2[] GetObstaclePoints()
    {
        List<Vector2> pointsList = new List<Vector2>();

        foreach(Polygon o in obstacles)
        {
            foreach(Vector2 p in o.Points)
            {
                pointsList.Add(p);
            }
        }

        return pointsList.ToArray();
    }

	public Vector2[][] GetWalkablePath()
	{
		return path;
	}

	// Draw a line representing the path.
	public void VisualizePath()
	{
		foreach(Vector2[] line in path)
		{
			Debug.DrawLine(new Vector3(line[0].x, line[0].y, 0), new Vector3(line[1].x, line[1].y, 0), Color.green);
		}
	}

	// First return value is the min X value, second is the max X value.
	public float[] GetPathXBounds()
	{
		float[] bounds = new float[2];
		bounds[0] = path[0][0].x;
		bounds[1] = path[0][0].x;
		foreach(Vector2[] line in path)
		{
			if (line[0].x < bounds[0])
			{
				bounds[0] = line[0].x;
			}
			if (line[0].x > bounds[1])
			{
				bounds[1] = line[0].x;
			}
			if (line[1].x < bounds[0])
			{
				bounds[0] = line[1].x;
			}
			if (line[1].x > bounds[1])
			{
				bounds[1] = line[1].x;
			}
		}
		return bounds;
	}

	// First return value is the min Y value, second is the max Y value.
	public float[] GetPathYBounds()
	{
		float[] bounds = new float[2];
		bounds[0] = path[0][0].y;
		bounds[1] = path[0][0].y;
		foreach(Vector2[] line in path)
		{
			if (line[0].y < bounds[0])
			{
				bounds[0] = line[0].y;
			}
			if (line[0].y > bounds[1])
			{
				bounds[1] = line[0].y;
			}
			if (line[1].y < bounds[0])
			{
				bounds[0] = line[1].y;
			}
			if (line[1].y > bounds[1])
			{
				bounds[1] = line[1].y;
			}
		}
		return bounds;
	}

	// Check if the point is on any of the lines that are part of the path.
	public bool CheckPointOnPath(Vector2 point)
	{
		float float_tol = 0.0001f;
		foreach(Vector2[] line in path)
		{
			// line has the start and end points of the line, so we need to check if the point is on the line.
			if (line[0].x > line[1].x)
			{
				// If within range, or if they're approximately equal.
				if ((point.x <= line[0].x && point.x >= line[1].x) || Math.Abs(point.x - line[0].x) < float_tol || Math.Abs(point.x - line[1].x) < float_tol)
				{
					if (line[0].y > line[1].y)
					{
						if ((point.y <= line[0].y && point.y >= line[1].y) || Math.Abs(point.y - line[0].y) < float_tol || Math.Abs(point.y - line[1].y) < float_tol)
						{
							return true;
						}
					}
					else
					{
						if ((point.y >= line[0].y && point.y <= line[1].y) || Math.Abs(point.y - line[0].y) < float_tol || Math.Abs(point.y - line[1].y) < float_tol)
						{
							return true;
						}
					}
				}
			}
			else
			{
				if ((point.x >= line[0].x && point.x <= line[1].x) || Math.Abs(point.x - line[0].x) < float_tol || Math.Abs(point.x - line[1].x) < float_tol)
				{
					if (line[0].y > line[1].y)
					{
						if ((point.y <= line[0].y && point.y >= line[1].y) || Math.Abs(point.y - line[0].y) < float_tol || Math.Abs(point.y - line[1].y) < float_tol)
						{
							return true;
						}
					}
					else
					{
						if ((point.y >= line[0].y && point.y <= line[1].y) || Math.Abs(point.y - line[0].y) < float_tol || Math.Abs(point.y - line[1].y) < float_tol)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Check if the point is on any of the lines that are part of the path.
	// public bool CheckPointOnPath(Vector2 point)
	// {
	// 	float float_tol = 0.0001f;
	// 	float tol = 0.05f;
	// 	Debug.Log("Point: " + point);
	// 	foreach(Vector2[] line in path)
	// 	{
	// 		// line has the start and end points of the line, so we need to check if the point is on the line.
	// 		if (line[0].x > line[1].x)
	// 		{
	// 			// If within range, or if they're approximately equal.
	// 			if ((point.x - line[0].x < tol && point.x - line[1].x > -tol) || Math.Abs(point.x - line[0].x) < float_tol || Math.Abs(point.x - line[1].x) < float_tol)
	// 			{
	// 				if (line[0].y > line[1].y)
	// 				{
	// 					if ((point.y - line[0].y < tol && point.y - line[1].y > -tol) || Math.Abs(point.y - line[0].y) < float_tol || Math.Abs(point.y - line[1].y) < float_tol)
	// 					{
	// 						return true;
	// 					}
	// 				}
	// 				else
	// 				{
	// 					if ((point.y - line[0].y > -tol && point.y - line[1].y < tol) || Math.Abs(point.y - line[0].y) < float_tol || Math.Abs(point.y - line[1].y) < float_tol)
	// 					{
	// 						return true;
	// 					}
	// 				}
	// 			}
	// 		}
	// 		else
	// 		{
	// 			if ((point.x - line[0].x > -tol && point.x - line[1].x < tol) || Math.Abs(point.x - line[0].x) < float_tol || Math.Abs(point.x - line[1].x) < float_tol)
	// 			{
	// 				if (line[0].y > line[1].y)
	// 				{
	// 					if ((point.y - line[0].y < tol && point.y - line[1].y > -tol) || Math.Abs(point.y - line[0].y) < float_tol || Math.Abs(point.y - line[1].y) < float_tol)
	// 					{
	// 						return true;
	// 					}
	// 				}
	// 				else
	// 				{
	// 					if ((point.y - line[0].y > -tol && point.y - line[1].y < tol) || Math.Abs(point.y - line[0].y) < float_tol || Math.Abs(point.y - line[1].y) < float_tol)
	// 					{
	// 						return true;
	// 					}
	// 				}
	// 			}
	// 		}
	// 	}
	// 	Debug.Log("Not on path");
	// 	return false;
	// }

	// Get nearest point on the path to the given point.
	public Vector3 GetCorrectedTarget(Vector3 direction, Vector3 nextNode)
	{
		// If direction is up or down, find nearest point by looking at the nearest x value.
		if (direction == Vector3.up || direction == Vector3.down)
		{
			float nearestX = 0;
			float nearestDist = float.MaxValue;
			foreach(Vector2[] line in path)
			{
				float x = line[0].x;
				float dist = Math.Abs(x - nextNode.x);
				if (dist < nearestDist)
				{
					nearestDist = dist;
					nearestX = x;
				}
			}
			return new Vector3(nearestX, nextNode.y, 0);
		}
		// If direction is left or right, find nearest point by looking at the nearest y value.
		else
		{
			float nearestY = 0;
			float nearestDist = float.MaxValue;
			foreach(Vector2[] line in path)
			{
				float y = line[0].y;
				float dist = Math.Abs(y - nextNode.y);
				if (dist < nearestDist)
				{
					nearestDist = dist;
					nearestY = y;
				}
			}
			return new Vector3(nextNode.x, nearestY, 0);
		}
	}
}
