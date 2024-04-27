using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // Create a singleton refernce.
    public static GameManager instance;

    // Reference to the player behavior script.
    private PlayerBehavior pB;

    // String that stores the scene the player is actively in.
    private string currentScene;

    [Header("Enemy Spawning")]
    [SerializeField] private GameObject[] enemies;
    private int flyingDemonPerWave = 2;
    private int crawlerPerWave = 3;
    private int speederPerWave = 2;

    void Start()
    {
        // Set singleton reference.
        instance = this;

        // Set player behavior script reference.
        pB = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();

        // Subscribe to the player game over event.
        pB.gameIsOver += HandleGameOver;

        // Set the currentscene the player is in to the one they are actively in.
        currentScene = SceneManager.GetActiveScene().name;

        // Invoke the spawning enemies function and repeat it every 15 seconds.
        InvokeRepeating("SpawnEnemies", 2f, 15f);

        // Load the player data
        LoadPlayer();
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
            // If the scene names does not exist/isn't in the build, throw an exception.
            else
            {
                throw new UnityException("It is not in build settings or simply doesn't exist.");
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

    // Spawn waves of enemies (call it using invoke repeating to set start and repeat time).
    private void SpawnEnemies()
    {
        GameObject enemiesHousing = new GameObject("Enemies");
        // As long as i is less than flyingDemonPerWave, spawn a flying demon enemy.
        for (int i = 0; i < flyingDemonPerWave; i++)
        {
            // Generate a random spawn on the X and the Y based on the map's width and height with a slight adjustment for borders.
            float randomSpawnX = Random.Range(-CreateMap.mapWidth + 15, CreateMap.mapWidth - 15);
            float randomSpawnY = Random.Range(-CreateMap.mapHeight + 15, CreateMap.mapHeight - 15);

            // Spawn the first enemy in the array (flying demon).
            Instantiate(enemies[0], new Vector3(randomSpawnX, randomSpawnY, 1f), transform.rotation, enemiesHousing.transform);
        }

        // As long as i is less than crawlerPerWave, spawn a crawler enemy.
        for (int i = 0; i < crawlerPerWave; i++)
        {
            // Generate a random spawn on the X and the Y based on the map's width and height with a slight adjustment for borders.
            float randomSpawnX = Random.Range(-CreateMap.mapWidth + 15, CreateMap.mapWidth - 15);
            float randomSpawnY = Random.Range(-CreateMap.mapHeight + 15, CreateMap.mapHeight - 15);

            // Spawn the second enemy in the array (crawler).
            Instantiate(enemies[1], new Vector3(randomSpawnX, randomSpawnY, 1f), transform.rotation, enemiesHousing.transform);
        }

        // As long as i is less than speederPerWave, spawn a speeder enemy.
        for (int i = 0; i < speederPerWave; i++)
        {
            // Generate a random spawn on the X and the Y based on the map's width and height with a slight adjustment for borders.
            float randomSpawnX = Random.Range(-CreateMap.mapWidth + 15, CreateMap.mapWidth - 15);
            float randomSpawnY = Random.Range(-CreateMap.mapHeight + 15, CreateMap.mapHeight - 15);

            // Spawn the second enemy in the array (speeder).
            Instantiate(enemies[2], new Vector3(randomSpawnX, randomSpawnY, 1f), transform.rotation, enemiesHousing.transform);
        }
    }

    // Save the player data.
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(pB);
    }

    // Load the player data.
    private void LoadPlayer()
    {
        try
        {
            PlayerData data = SaveSystem.LoadPlayer();

            // Set the player script variables to the data loaded.
            pB.Lives = data.lives;
            pB.Score = data.score;

            // Create a vector 3 that takes the x, y, and z info stored in the position array that was created in PlayerData.
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            pB.transform.position = position;

            // Set player's class name.
            pB.className = data.playerClass;

            // Set the player UI.
            pB.SetUI();

            //// Set the map biome and size.
            //CreateMap.chosenLevel = data.biome;
            //CreateMap.mapWidth = data.mapWidth;
            //CreateMap.mapHeight = data.mapHeight;
        }
        catch (Exception)
        {
            //Debug.LogWarning("No save data found!");
        }
        finally
        {
            pB.PlayerClass();
        }
    }
}