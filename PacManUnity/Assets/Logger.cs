using UnityEngine;
using System;
using System.IO;

public class Logger : MonoBehaviour
{
    public PelletHandler pelletHandler;
    public GameHandler gameHandler;
    public string filePath = "log";
    private float nextTime = 0.0f;
    private int gameNumber = 0;
    private float timeFromStart = 0.0f;
    private GraphNode ghost;
    private GraphNode pacman;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        filePath = filePath + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".csv";
        File.WriteAllText(filePath, "Time, Pellets, Tension, Distance, Reward\n");
        string text = string.Format("Game {0}\n", gameNumber);
        File.AppendAllText(filePath, text);
        ghost = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(gameHandler.GetState().ghostPosition);
        pacman = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(gameHandler.GetState().agentPosition);
    }

    // Update is called once per frame
    void Update()
    {
        ghost = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(gameHandler.GetState().ghostPosition);
        pacman = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(gameHandler.GetState().agentPosition);
        Vector3[] path = HW3NavigationHandler.Instance.PathFinder.CalculatePath(pacman, ghost);
        print(path);
        float distance = path.Length;
        float tension = gameHandler.CalculateTensionReward(distance, Config.TENSION_MEAN, Config.TENSION_STD_DEV);
        string text = string.Format("{0}, {1}, {2}, {3}, {4}\n", Time.time - timeFromStart, pelletHandler.NumPellets, tension, distance, gameHandler.currReward);
        print(text);
        File.AppendAllText(filePath, text);
    }

    public void Reset(){
        gameNumber++;
        string text = string.Format("Game {0}\n", gameNumber);
        print(text);
        timeFromStart = Time.time;
        File.AppendAllText(filePath, text);
    }
}
