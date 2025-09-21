using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this to work with TextMeshPro
using UnityEngine.SceneManagement; // Add this to work with SceneManager to load or reload scenes

public class ScoreManager : MonoBehaviour
{
    // Public statics variables accessible from any script but can't be seen in Inspector
    public static bool gameOver;
    public static bool won;
    public static int score;

    public TMP_Text textbox; // Set this in Inspector

    public int scoreToWin; // Set this in Inspector (score needed to win)

    // Start is called before the first frame update
    void Start()
    {
        // Set initial values for variables
        gameOver = false;
        won = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            textbox.text = "Score: " + score;
        }

        if(score >= scoreToWin)
        {
            won = true;
            gameOver = true;
        }

        if(gameOver)
        {
            if(won) // Win case
            {
                textbox.text = "You win!\nPress R to try again";
            }
            else // Lose case
            {
                textbox.text = "You lose!\nPress R to try again";
            }

            // Reload case
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
