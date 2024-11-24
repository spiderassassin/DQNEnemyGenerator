using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public static ScoreHandler Instance;

    public Text scoreText;
    private int score = 0;

    public int Score { get { return score; } }//Public reference for Score

    [SerializeField] private GameHandler gameHandler;
    [SerializeField] private bool restartOnDeath;

    private const string BASIC_SCORE_TEXT = "Pellets: ";

    void Start()
    {
        Instance = this;
    }

    public void UpdateScore()
    {
        score += 1;
        scoreText.text = BASIC_SCORE_TEXT+score;

        // Check if game beaten.
        if (PelletHandler.Instance.NumPellets == 0)
        {
            // Give a reward.
            Debug.Log("You win!");
            
            // Stop gameplay.
            Time.timeScale = 0;
        }
    }

    public void KillPacman()
    {
        // Give negative reward.
        Debug.Log("You lose!");

        // Stop gameplay.
        Time.timeScale = 0;

        // Only do this if we're recording demonstrations for BHC.
        if (restartOnDeath)
        {
            // Restart the game.
            gameHandler.ResetGame();
        }

    }
}
