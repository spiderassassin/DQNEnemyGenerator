using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    private bool gameOver = false;

    public int Score { get { return score; } }//Public reference for Score
    public bool GameOver { get { return gameOver; } set { gameOver = value; } } //Public reference for GameOver

    private const string BASIC_SCORE_TEXT = "Pellets: ";

    [SerializeField] private PelletHandler pelletHandler;

    void Start()
    {

    }

    public void UpdateScore()
    {
        score += 1;
        // scoreText.text = BASIC_SCORE_TEXT+score;

        // Check if game beaten.
        if (pelletHandler.NumPellets == 0)
        {
            // Give a reward.
            Debug.Log("You win!");
            
            gameOver = true;
        }
    }

    public void KillPacman()
    {
        // Give negative reward.
        Debug.Log("You lose!");

        gameOver = true;
    }
}
