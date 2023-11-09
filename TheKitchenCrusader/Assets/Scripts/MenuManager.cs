using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    /// <summary>
    /// Starts the game and takes player to next level
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Scene0");

        // Confirm the scene has been loaded
        Debug.Log("Game has started. Have Fun.");

        // if game is paused 
        if (Time.timeScale == 0)
        {
            // resume game's runtime
            Time.timeScale = 1;
        }
    }
    /// <summary>
    /// Go to main menu
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
        // if game is paused 
        if (Time.timeScale == 0)
        {
            // resume game's runtime
            Time.timeScale = 1;
        }
    }
    /// <summary>
    /// Quits the game on the main menu
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
