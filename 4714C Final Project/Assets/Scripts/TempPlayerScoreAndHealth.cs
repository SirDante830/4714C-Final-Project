using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempPlayerScoreAndHealth : MonoBehaviour
{
    // Variables for lives and score.
    private int maxLives = 20;
    private int _lives = 0;
    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
        }
    }

    private int _score = 0;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    [Header("UI Text")]
    // UI Text variables.
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Set player lives to the max at the start. Do this before setting UI so it is up to date.
        Lives = maxLives;

        // Run at start to make sure UI is displayed when player begins the game.
        SetUI();        
    }

    // Function to make sure lives and score do not go outside their boundaries.
    void MinAndMaxChecks()
    {
        // If lives somehow go lower than 0, set them back to 0.
        if (_lives < 0)
        {
            _lives = 0;
        }

        // If lives somehow go over the max, keep them at max.
        else if (_lives > maxLives)
        {
            _lives = maxLives;
        }

        // If score somehow goes below 0, set it back to 0.
        if (_score < 0)
        {
            _score = 0;
        }
    }

    // Function to change score.
    public void ChangeScore(int scoreChange)
    {
       // Adjust score by the change amount.
        Score += scoreChange;

        // Run a check to make sure the max or minimum of lives and score are not hit. *Score does not have a max.
        MinAndMaxChecks();

        // Set the UI so it changes when score changes.
        SetUI();
    }

    // Function to change lives.
    public void ChangeLives(int livesChange)
    {
        // Adjust the lives by the change amount.
        Lives += livesChange;
        
        // Run a check to make sure the max or minimum of lives and score are not hit. *Score does not have a max.
        MinAndMaxChecks();
        
        // Set the UI so it changes when the lives change.
        SetUI();
    }

    // Function to set UI.
    void SetUI()
    {
        // Set the livesText to the text in "" + the current lives variable value.
        livesText.text = "Lives: " + _lives;

        // Set the scoreText to the text in "" + the current score variable value.
        scoreText.text = "Score: " + _score;
    }
}