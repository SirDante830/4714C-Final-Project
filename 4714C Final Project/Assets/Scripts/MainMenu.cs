using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Variables for snow biome size and obstacles.
    private float snowMinMapSize = 9.0f;
    private float snowMaxMapSize = 14.0f;
    private int snowMinObstaclesAmount = 30;
    private int snowMaxObstaclesAmount = 50;

    // Variables for grassland biome size and obstacles.
    private float grasslandMinMapSize = 12.5f;
    private float grasslandMaxMapSize = 17.5f;
    private int grasslandMinObstaclesAmount = 45;
    private int grasslandMaxObstaclesAmount = 65;

    // Variables for desert biome size and obstacles.
    private float desertMinMapSize = 7.5f;
    private float desertMaxMapSize = 12.5f;
    private int desertMinObstaclesAmount = 25;
    private int desertMaxObstaclesAmount = 40;


    private void Start()
    {
        // Limit frame rate to the refresh rate of the monitor.
        QualitySettings.vSyncCount = 1;
    }

    // Load the game scene with a snow biome.
    public void PlaySnowBiome()
    {
        // Set chosen level, size of the map, and number of obstacles.
        CreateMapandType.chosenLevel = CreateMapandType.levelTypes.Snow;
        CreateMapandType.mapSize = Random.Range(snowMinMapSize, snowMaxMapSize);
        CreateMapandType.obstaclesToSpawn = Random.Range(snowMinObstaclesAmount, snowMaxObstaclesAmount);
        LoadScene("Game");
    }

    // Load the game scene with a grassland biome.
    public void PlayGrasslandBiome()
    {
        // Set chosen level, size of the map, and number of obstacles.
        CreateMapandType.chosenLevel = CreateMapandType.levelTypes.Grassland;
        CreateMapandType.mapSize = Random.Range(grasslandMinMapSize, grasslandMaxMapSize);
        CreateMapandType.obstaclesToSpawn = Random.Range(grasslandMinObstaclesAmount, grasslandMaxObstaclesAmount);
        LoadScene("Game");
    }

    // Load the game scene with a desert biome.
    public void PlayDesertBiome()
    {
        // Set chosen level, size of the map, and number of obstacles.
        CreateMapandType.chosenLevel = CreateMapandType.levelTypes.Desert;
        CreateMapandType.mapSize = Random.Range(desertMinMapSize, desertMaxMapSize);
        CreateMapandType.obstaclesToSpawn = Random.Range(desertMinObstaclesAmount, desertMaxObstaclesAmount);
        LoadScene("Game");
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("Credits");
    }

    // This function will make it so if you press the quit button, the application will close.
    public void QuitButton()
    {
        Application.Quit();
    }

    // Function that loads the scene.
    void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
