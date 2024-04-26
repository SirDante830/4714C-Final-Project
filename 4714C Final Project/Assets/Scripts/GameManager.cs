using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Reference to the player behavior script.
    private PlayerBehavior pB;

    // String that stores the scene the player is actively in.
    private string currentScene;

    void Start()
    {
        // Set player behavior script reference.
        pB = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();

        // Subscribe to the player game over event.
        pB.gameIsOver += HandleGameOver;

        // Set the currentscene the playeris in to the one they are actively in.
        currentScene = SceneManager.GetActiveScene().name;
        Debug.Log(currentScene);
    }
    // Function that loads the inputted scene string name
    void LoadScene(string sceneName)
    {
        // Try to load the scene, if there is an error, load the main menu.
        try
        {
            // If the scene name exists/is valid, load the scene.
            if (SceneManager.GetSceneByName(sceneName).IsValid())
            {
                SceneManager.LoadScene(sceneName);
            }
            // If the scene names does not exit/ isn't in the build, throw an exception.
            else
            {
                throw new UnityException(" It is not in build settings or simply doesn't exist.");
            }
        }
        // If UnityException is caught, default to loading the main menu and send a message.
        catch (UnityException)
        {
            SceneManager.LoadScene("MainMenu");
            //Debug.Log("Couldn't load '" + sceneName + "'. " + exception.Message);
        }
        // Say scene loaded whether the desired scene or default scene is loaded.
        finally
        {
            //Debug.Log("Scene loaded.");
        }
    }

    void HandleGameOver()
    {
        // Reload the current scene so the player can try again and resume time.
        LoadScene(currentScene);
        Time.timeScale = 1.0f;
    }
}