using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    public static int GRID_WIDTH = 28;  // 29
    public static int GRID_HEIGHT = 32;  // 33
    public static float GRID_INTERVAL = 0.15f;
    public static float GRID_INSET = 0.06f;

    public static float PELLET_INTERVAL = 0.15f;

    public static float AGENT_MOVE_INTERVAL = 0.01f;

    public static float PATH_TOL = 0.05f;

    public static int TENSION_DISTANCE = 5;
    public static float TENSION_MEAN = 0.65f;
    public static float TENSION_STD_DEV = 0.1f;
}