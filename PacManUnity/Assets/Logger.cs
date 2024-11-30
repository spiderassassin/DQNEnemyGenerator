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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        filePath = filePath + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".csv";
        File.WriteAllText(filePath, "Time, Pellets, Tension, Position, Distance, Reward\n");
        string text = string.Format("Game {0}\n", gameNumber);
        File.AppendAllText(filePath, text);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextTime){
            nextTime += 0.1f;
            Vector3 position = gameHandler.GetState().ghostPosition;
            Vector3 pacman = gameHandler.GetState().agentPosition;
            float distance = Math.Abs(position[0]-pacman[0]) + Math.Abs(position[1]-pacman[1]) + Math.Abs(position[2]-pacman[2]);

            string text = string.Format("{0}, {1}, {2}, ({3} {4} {5}), {6}, {7}\n", Time.time, pelletHandler.NumPellets, gameHandler.accTension, position[0], position[1], position[2], distance, gameHandler.currReward);
            print(text);
            File.AppendAllText(filePath, text);
        }
    }

    public void Reset(){
        gameNumber++;
        string text = string.Format("Game {0}\n", gameNumber);
        print(text);
        File.AppendAllText(filePath, text);
    }
}
