using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTest1Points : ObstacleDefiner
{
	// Grid representation, array of strings, where "X" is a wall, "O" is a path.
	// Careful, for visual clarity here, coordinates are stored as (y, x) instead of (x, y).
	string[] grid = new string[]
    {
        "XXXXXXXXXX",
        "XOOOOOOOOX",
        "XXXOXXOXXX",
        "XOXOXXOXOX",
        "XOOOOOOOOX",
        "XOXXXOXXOX",
        "XOOOOOOOOX",
        "XOXXXXXXOX",
        "XOOOOOOOOX",
        "XXXXXXXXXX"
    };

    public override Vector2[][] GetWalkablePath()
    {
        List<Vector2[]> arrayOfWalkablePaths = new List<Vector2[]>();

        // Calculate the starting node position.
        float startX = -(Config.GRID_WIDTH / 2) * Config.GRID_INTERVAL;
        float startY = (Config.GRID_HEIGHT / 2) * Config.GRID_INTERVAL - Config.GRID_INTERVAL;

        // Loop through each node in the grid.
        for (int y = 0; y < Config.GRID_HEIGHT; y++)
        {
            for (int x = 0; x < Config.GRID_WIDTH; x++)
            {
                // Only process nodes that contain paths.
                if (grid[y][x] == 'O')
                {
                    // Add vectors representing straight line segments within the node.
                    // This changes depending on whether the neighbouring nodes are walls or paths.
                    // Check top. If node above is a path, add a line segment from the center of the node to the top.
                    if (y != 0 && grid[y - 1][x] == 'O')
                    {
                        arrayOfWalkablePaths.Add(new Vector2[]
                        {
                            new Vector2(startX + x * Config.GRID_INTERVAL, startY - y * Config.GRID_INTERVAL),
                            new Vector2(startX + x * Config.GRID_INTERVAL, startY - y * Config.GRID_INTERVAL + Config.GRID_INTERVAL / 2)
                        });
                    }
                    // Check bottom. If node below is a path, add a line segment from the center of the node to the bottom.
                    if (y != Config.GRID_HEIGHT - 1 && grid[y + 1][x] == 'O')
                    {
                        arrayOfWalkablePaths.Add(new Vector2[]
                        {
                            new Vector2(startX + x * Config.GRID_INTERVAL, startY - y * Config.GRID_INTERVAL),
                            new Vector2(startX + x * Config.GRID_INTERVAL, startY - y * Config.GRID_INTERVAL - Config.GRID_INTERVAL / 2)
                        });
                    }
                    // Check left. If node to the left is a path, add a line segment from the center of the node to the left.
                    if (x != 0 && grid[y][x - 1] == 'O')
                    {
                        arrayOfWalkablePaths.Add(new Vector2[]
                        {
                            new Vector2(startX + x * Config.GRID_INTERVAL, startY - y * Config.GRID_INTERVAL),
                            new Vector2(startX + x * Config.GRID_INTERVAL - Config.GRID_INTERVAL / 2, startY - y * Config.GRID_INTERVAL)
                        });
                    }
                    // Check right. If node to the right is a path, add a line segment from the center of the node to the right.
                    if (x != Config.GRID_WIDTH - 1 && grid[y][x + 1] == 'O')
                    {
                        arrayOfWalkablePaths.Add(new Vector2[]
                        {
                            new Vector2(startX + x * Config.GRID_INTERVAL, startY - y * Config.GRID_INTERVAL),
                            new Vector2(startX + x * Config.GRID_INTERVAL + Config.GRID_INTERVAL / 2, startY - y * Config.GRID_INTERVAL)
                        });
                    }
                }
            }
        }

        return arrayOfWalkablePaths.ToArray();
    }

	public override Vector2[][] GetObstaclePoints()
	{
        List<Vector2[]> arrayOfObstaclePoints = new List<Vector2[]>();

        // Calculate the starting offset to center the grid in the world.
        float startX = -(Config.GRID_WIDTH / 2) * Config.GRID_INTERVAL - Config.GRID_INTERVAL / 2;
        float startY = (Config.GRID_HEIGHT / 2) * Config.GRID_INTERVAL - Config.GRID_INTERVAL / 2;

        xBound = (Config.GRID_WIDTH / 2 - 1) * Config.GRID_INTERVAL;
        yBound = (Config.GRID_HEIGHT / 2 - 2) * Config.GRID_INTERVAL;

        // Loop through each cell in the grid.
        for (int y = 0; y < Config.GRID_HEIGHT; y++)
        {
            for (int x = 0; x < Config.GRID_WIDTH; x++)
            {
                // Only process cells that contain obstacles.
                if (grid[y][x] == 'X')
                {
                    float posX = startX + x * Config.GRID_INTERVAL;
                    float posY = startY - y * Config.GRID_INTERVAL;

                    // Check neighboring cells to add line segments where needed, with inset adjustments.
                    // Top edge.
                    if (y == 0 || grid[y - 1][x] == 'O'
						|| (x != 0 && grid[y - 1][x] == 'X' && grid[y][x - 1] == 'X' && grid[y - 1][x - 1] == 'O')  // Diagonally to the left.
						|| (x != Config.GRID_WIDTH - 1 && grid[y - 1][x] == 'X' && grid[y][x + 1] == 'X' && grid[y - 1][x + 1] == 'O'))  // Diagonally to the right.
                    {
						float startPosX = posX;
						float endPosX = posX + Config.GRID_INTERVAL;
						// Check if this is a corner and account for inset if so.
						if (x == 0 || grid[y][x - 1] == 'O')
						{
							startPosX += Config.GRID_INSET;
						}
						if (x == Config.GRID_WIDTH - 1 || grid[y][x + 1] == 'O')
						{
							endPosX -= Config.GRID_INSET;
						}
						if (y != 0 && x != 0 && grid[y - 1][x] == 'X' && grid[y][x - 1] == 'X' && grid[y - 1][x - 1] == 'O')
						{
							endPosX = posX + Config.GRID_INSET;
						}
						if (y != 0 && x != Config.GRID_WIDTH - 1 && grid[y - 1][x] == 'X' && grid[y][x + 1] == 'X' && grid[y - 1][x + 1] == 'O')
						{
							startPosX = posX + Config.GRID_INSET;
						}
                        arrayOfObstaclePoints.Add(new Vector2[]
                        {
                            new Vector2(startPosX, posY - Config.GRID_INSET),
                            new Vector2(endPosX, posY - Config.GRID_INSET)
                        });
                    }
                    // Bottom edge.
                    if (y == Config.GRID_HEIGHT - 1 || grid[y + 1][x] == 'O'
						|| (x != 0 && grid[y + 1][x] == 'X' && grid[y][x - 1] == 'X' && grid[y + 1][x - 1] == 'O')  // Diagonally to the left.
						|| (x != Config.GRID_WIDTH - 1 && grid[y + 1][x] == 'X' && grid[y][x + 1] == 'X' && grid[y + 1][x + 1] == 'O'))  // Diagonally to the right.
                    {
						float startPosX = posX;
						float endPosX = posX + Config.GRID_INTERVAL;
						// Check if this is a corner and account for inset if so.
						if (x == 0 || grid[y][x - 1] == 'O')
						{
							startPosX += Config.GRID_INSET;
						}
						if (x == Config.GRID_WIDTH - 1 || grid[y][x + 1] == 'O')
						{
							endPosX -= Config.GRID_INSET;
						}
						if (y != Config.GRID_HEIGHT - 1 && x != 0 && grid[y + 1][x] == 'X' && grid[y][x - 1] == 'X' && grid[y + 1][x - 1] == 'O')
						{
							endPosX = posX + Config.GRID_INSET;
						}
						if (y != Config.GRID_HEIGHT - 1 && x != Config.GRID_WIDTH - 1 && grid[y + 1][x] == 'X' && grid[y][x + 1] == 'X' && grid[y + 1][x + 1] == 'O')
						{
							startPosX = posX + Config.GRID_INSET;
						}
                        arrayOfObstaclePoints.Add(new Vector2[]
                        {
                            new Vector2(startPosX, posY - Config.GRID_INTERVAL + Config.GRID_INSET),
                            new Vector2(endPosX, posY - Config.GRID_INTERVAL + Config.GRID_INSET)
                        });
                    }
                    // Left edge.
                    if (x == 0 || grid[y][x - 1] == 'O'
						|| (y != 0 && grid[y - 1][x] == 'X' && grid[y - 1][x - 1] == 'O' && grid[y][x - 1] == 'X')  // Diagonally to the top.
						|| (y != Config.GRID_HEIGHT - 1 && grid[y + 1][x] == 'X' && grid[y + 1][x - 1] == 'O' && grid[y][x - 1] == 'X'))  // Diagonally to the bottom.
                    {
						float startPosY = posY;
						float endPosY = posY - Config.GRID_INTERVAL;
						// Check if this is a corner and account for inset if so.
						if (y == 0 || grid[y - 1][x] == 'O')
						{
							startPosY -= Config.GRID_INSET;
						}
						if (y == Config.GRID_HEIGHT - 1 || grid[y + 1][x] == 'O')
						{
							endPosY += Config.GRID_INSET;
						}
						if (y != 0 && x != 0 && grid[y - 1][x] == 'X' && grid[y - 1][x - 1] == 'O' && grid[y][x - 1] == 'X')
						{
							endPosY = posY - Config.GRID_INSET;
						}
						if (y != Config.GRID_HEIGHT - 1 && x != 0 && grid[y + 1][x] == 'X' && grid[y + 1][x - 1] == 'O' && grid[y][x - 1] == 'X')
						{
							startPosY = posY - Config.GRID_INSET;
						}
                        arrayOfObstaclePoints.Add(new Vector2[]
                        {
                            new Vector2(posX + Config.GRID_INSET, startPosY),
                            new Vector2(posX + Config.GRID_INSET, endPosY)
                        });
                    }
                    // Right edge.
                    if (x == Config.GRID_WIDTH - 1 || grid[y][x + 1] == 'O'
						|| (y != 0 && grid[y - 1][x] == 'X' && grid[y - 1][x + 1] == 'O' && grid[y][x + 1] == 'X')  // Diagonally to the top.
						|| (y != Config.GRID_HEIGHT - 1 && grid[y + 1][x] == 'X' && grid[y + 1][x + 1] == 'O' && grid[y][x + 1] == 'X'))  // Diagonally to the bottom.
                    {
						float startPosY = posY;
						float endPosY = posY - Config.GRID_INTERVAL;
						// Check if this is a corner and account for inset if so.
						if (y == 0 || grid[y - 1][x] == 'O')
						{
							startPosY -= Config.GRID_INSET;
						}
						if (y == Config.GRID_HEIGHT - 1 || grid[y + 1][x] == 'O')
						{
							endPosY += Config.GRID_INSET;
						}
						if (y != 0 && x != Config.GRID_WIDTH - 1 && grid[y - 1][x] == 'X' && grid[y - 1][x + 1] == 'O' && grid[y][x + 1] == 'X')
						{
							endPosY = posY - Config.GRID_INSET;
						}
						if (y != Config.GRID_HEIGHT - 1 && x != Config.GRID_WIDTH - 1 && grid[y + 1][x] == 'X' && grid[y + 1][x + 1] == 'O' && grid[y][x + 1] == 'X')
						{
							startPosY = posY - Config.GRID_INSET;
						}
                        arrayOfObstaclePoints.Add(new Vector2[]
                        {
                            new Vector2(posX + Config.GRID_INTERVAL - Config.GRID_INSET, startPosY),
                            new Vector2(posX + Config.GRID_INTERVAL - Config.GRID_INSET, endPosY)
                        });
                    }
                }
            }
        }

        return arrayOfObstaclePoints.ToArray();
    }
}
