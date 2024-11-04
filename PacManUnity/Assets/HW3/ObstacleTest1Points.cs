using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTest1Points : ObstacleDefiner
{
	// Needs to be the same as in Grid Handler.
	const int gridWidth = 28;
    const int gridHeight = 32;
    const float gridInterval = 0.12f;
	// Distance to inset lines from the cell edges.
    const float inset = 0.06f;
	// Grid representation, array of strings, where "X" is a wall, "O" is a path.
	// Careful, for visual clarity here, coordinates are stored as (y, x) instead of (x, y).
	string[] grid = new string[gridHeight]
    {
        "XXXXXXXXXXXXXXXXXXXXXXXXXXXX",
        "XOOOOOOOOOOOOXXOOOOOOOOOOOOX",
        "XOXXXXOXXXXXOXXOXXXXXOXXXXOX",
        "XOXXXXOXXXXXOXXOXXXXXOXXXXOX",
        "XOXXXXOXXXXXOXXOXXXXXOXXXXOX",
        "XOOOOOOOOOOOOOOOOOOOOOOOOOOX",
        "XOXXXXOXXOXXXXXXXXOXXOXXXXOX",
        "XOXXXXOXXOXXXXXXXXOXXOXXXXOX",
        "XOOOOOOXXOOOOXXOOOOXXOOOOOOX",
        "XXXXXXOXXXXXOXXOXXXXXOXXXXXX",
        "XXXXXXOXXXXXOXXOXXXXXOXXXXXX",
        "XXXXXXOXXOOOOOOOOOOXXOXXXXXX",
        "XXXXXXOXXOXXXXXXXXOXXOXXXXXX",
        "XXXXXXOXXOXXXXXXXXOXXOXXXXXX",
        "XOOOOOOOOOXXXXXXXXOOOOOOOOOX",
        "XXXXXXOXXOXXXXXXXXOXXOXXXXXX",
        "XXXXXXOXXOXXXXXXXXOXXOXXXXXX",
        "XXXXXXOXXOOOOOOOOOOXXOXXXXXX",
        "XXXXXXOXXOXXXXXXXXOXXOXXXXXX",
        "XXXXXXOXXOXXXXXXXXOXXOXXXXXX",
        "XOOOOOOOOOOOOXXOOOOOOOOOOOOX",
        "XOXXXXOXXXXXOXXOXXXXXOXXXXOX",
        "XOXXXXOXXXXXOXXOXXXXXOXXXXOX",
        "XOOOXXOOOOOOOOOOOOOOOOXXOOOX",
        "XXXOXXOXXOXXXXXXXXOXXOXXOXXX",
        "XXXOXXOXXOXXXXXXXXOXXOXXOXXX",
        "XOOOOOOXXOOOOXXOOOOXXOOOOOOX",
        "XOXXXXXXXXXXOXXOXXXXXXXXXXOX",
        "XOXXXXXXXXXXOXXOXXXXXXXXXXOX",
        "XOOOOOOOOOOOOOOOOOOOOOOOOOOX",
        "XXXXXXXXXXXXXXXXXXXXXXXXXXXX",
        "XXXXXXXXXXXXXXXXXXXXXXXXXXXX"
    };

	public override Vector2[][] GetObstaclePoints()
	{
        List<Vector2[]> arrayOfObstaclePoints = new List<Vector2[]>();

        // Calculate the starting offset to center the grid in the world.
        float startX = -(gridWidth / 2) * gridInterval - gridInterval / 2;
        float startY = (gridHeight / 2) * gridInterval - gridInterval / 2;

        // Loop through each cell in the grid.
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                // Only process cells that contain obstacles.
                if (grid[y][x] == 'X')
                {
                    float posX = startX + x * gridInterval;
                    float posY = startY - y * gridInterval;

                    // Check neighboring cells to add line segments where needed, with inset adjustments.
                    // Top edge.
                    if (y == 0 || grid[y - 1][x] == 'O'
						|| (x != 0 && grid[y - 1][x] == 'X' && grid[y][x - 1] == 'X' && grid[y - 1][x - 1] == 'O')  // Diagonally to the left.
						|| (x != gridWidth - 1 && grid[y - 1][x] == 'X' && grid[y][x + 1] == 'X' && grid[y - 1][x + 1] == 'O'))  // Diagonally to the right.
                    {
						float startPosX = posX;
						float endPosX = posX + gridInterval;
						// Check if this is a corner and account for inset if so.
						if (x == 0 || grid[y][x - 1] == 'O')
						{
							startPosX += inset;
						}
						if (x == gridWidth - 1 || grid[y][x + 1] == 'O')
						{
							endPosX -= inset;
						}
						if (y != 0 && x != 0 && grid[y - 1][x] == 'X' && grid[y][x - 1] == 'X' && grid[y - 1][x - 1] == 'O')
						{
							endPosX = posX + inset;
						}
						if (y != 0 && x != gridWidth - 1 && grid[y - 1][x] == 'X' && grid[y][x + 1] == 'X' && grid[y - 1][x + 1] == 'O')
						{
							startPosX = posX + inset;
						}
                        arrayOfObstaclePoints.Add(new Vector2[]
                        {
                            new Vector2(startPosX, posY - inset),
                            new Vector2(endPosX, posY - inset)
                        });
                    }
                    // Bottom edge.
                    if (y == gridHeight - 1 || grid[y + 1][x] == 'O'
						|| (x != 0 && grid[y + 1][x] == 'X' && grid[y][x - 1] == 'X' && grid[y + 1][x - 1] == 'O')  // Diagonally to the left.
						|| (x != gridWidth - 1 && grid[y + 1][x] == 'X' && grid[y][x + 1] == 'X' && grid[y + 1][x + 1] == 'O'))  // Diagonally to the right.
                    {
						float startPosX = posX;
						float endPosX = posX + gridInterval;
						// Check if this is a corner and account for inset if so.
						if (x == 0 || grid[y][x - 1] == 'O')
						{
							startPosX += inset;
						}
						if (x == gridWidth - 1 || grid[y][x + 1] == 'O')
						{
							endPosX -= inset;
						}
						if (y != gridHeight - 1 && x != 0 && grid[y + 1][x] == 'X' && grid[y][x - 1] == 'X' && grid[y + 1][x - 1] == 'O')
						{
							endPosX = posX + inset;
						}
						if (y != gridHeight - 1 && x != gridWidth - 1 && grid[y + 1][x] == 'X' && grid[y][x + 1] == 'X' && grid[y + 1][x + 1] == 'O')
						{
							startPosX = posX + inset;
						}
                        arrayOfObstaclePoints.Add(new Vector2[]
                        {
                            new Vector2(startPosX, posY - gridInterval + inset),
                            new Vector2(endPosX, posY - gridInterval + inset)
                        });
                    }
                    // Left edge.
                    if (x == 0 || grid[y][x - 1] == 'O'
						|| (y != 0 && grid[y - 1][x] == 'X' && grid[y - 1][x - 1] == 'O' && grid[y][x - 1] == 'X')  // Diagonally to the top.
						|| (y != gridHeight - 1 && grid[y + 1][x] == 'X' && grid[y + 1][x - 1] == 'O' && grid[y][x - 1] == 'X'))  // Diagonally to the bottom.
                    {
						float startPosY = posY;
						float endPosY = posY - gridInterval;
						// Check if this is a corner and account for inset if so.
						if (y == 0 || grid[y - 1][x] == 'O')
						{
							startPosY -= inset;
						}
						if (y == gridHeight - 1 || grid[y + 1][x] == 'O')
						{
							endPosY += inset;
						}
						if (y != 0 && x != 0 && grid[y - 1][x] == 'X' && grid[y - 1][x - 1] == 'O' && grid[y][x - 1] == 'X')
						{
							endPosY = posY - inset;
						}
						if (y != gridHeight - 1 && x != 0 && grid[y + 1][x] == 'X' && grid[y + 1][x - 1] == 'O' && grid[y][x - 1] == 'X')
						{
							startPosY = posY - inset;
						}
                        arrayOfObstaclePoints.Add(new Vector2[]
                        {
                            new Vector2(posX + inset, startPosY),
                            new Vector2(posX + inset, endPosY)
                        });
                    }
                    // Right edge.
                    if (x == gridWidth - 1 || grid[y][x + 1] == 'O'
						|| (y != 0 && grid[y - 1][x] == 'X' && grid[y - 1][x + 1] == 'O' && grid[y][x + 1] == 'X')  // Diagonally to the top.
						|| (y != gridHeight - 1 && grid[y + 1][x] == 'X' && grid[y + 1][x + 1] == 'O' && grid[y][x + 1] == 'X'))  // Diagonally to the bottom.
                    {
						float startPosY = posY;
						float endPosY = posY - gridInterval;
						// Check if this is a corner and account for inset if so.
						if (y == 0 || grid[y - 1][x] == 'O')
						{
							startPosY -= inset;
						}
						if (y == gridHeight - 1 || grid[y + 1][x] == 'O')
						{
							endPosY += inset;
						}
						if (y != 0 && x != gridWidth - 1 && grid[y - 1][x] == 'X' && grid[y - 1][x + 1] == 'O' && grid[y][x + 1] == 'X')
						{
							endPosY = posY - inset;
						}
						if (y != gridHeight - 1 && x != gridWidth - 1 && grid[y + 1][x] == 'X' && grid[y + 1][x + 1] == 'O' && grid[y][x + 1] == 'X')
						{
							startPosY = posY - inset;
						}
                        arrayOfObstaclePoints.Add(new Vector2[]
                        {
                            new Vector2(posX + gridInterval - inset, startPosY),
                            new Vector2(posX + gridInterval - inset, endPosY)
                        });
                    }
                }
            }
        }

        return arrayOfObstaclePoints.ToArray();
    }
}
