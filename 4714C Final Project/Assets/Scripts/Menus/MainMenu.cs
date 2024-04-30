using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Variables for snow biome size and obstacles.
    private float snowMinMapSize = 50.0f;
    private float snowMaxMapSize = 80.0f;
    private int snowMinObstaclesAmount = 40;
    private int snowMaxObstaclesAmount = 65;

    // Variables for grassland biome size and obstacles.
    private float grasslandMinMapSize = 65f;
    private float grasslandMaxMapSize = 100f;
    private int grasslandMinObstaclesAmount = 60;
    private int grasslandMaxObstaclesAmount = 85;

    // Variables for desert biome size and obstacles.
    private float desertMinMapSize = 85f;
    private float desertMaxMapSize = 150f;
    private int desertMinObstaclesAmount = 60;
    private int desertMaxObstaclesAmount = 100;


    private void Start()
    {
        // Limit frame rate to the refresh rate of the monitor.
        QualitySettings.vSyncCount = 1;
    }

    // Load the game scene with a snow biome.
    public void PlaySnowBiome()
    {
        // Set chosen level, number of obstacles, and size of the map.
        CreateMap.chosenLevel = CreateMap.levelTypes.Snow;
        CreateMap.obstaclesToSpawn = Random.Range(snowMinObstaclesAmount, snowMaxObstaclesAmount);

        // Since map has a separate width and height, use the same number for both.
        // Maybe change this to do separate width and height for more variance.
        float randomSize = Random.Range(snowMinMapSize, snowMaxMapSize);
        CreateMap.mapWidth = (int)randomSize;
        CreateMap.mapHeight = (int)randomSize;

        // Reset player attack damage.
        PlayerAttack.damageDealt = 25;
        PlayerAttack.bombDamageDealt = 35;

        LoadScene("Game");
    }

    // Load the game scene with a grassland biome.
    public void PlayGrasslandBiome()
    {
        // Set chosen level, number of obstacles, and size of the map.
        CreateMap.chosenLevel = CreateMap.levelTypes.Grassland;
        CreateMap.obstaclesToSpawn = Random.Range(grasslandMinObstaclesAmount, grasslandMaxObstaclesAmount);

        // Since map has a separate width and height, use the same number for both.
        // Maybe change this to do separate width and height for more variance.
        float randomSize = Random.Range(grasslandMinMapSize, grasslandMaxMapSize);
        CreateMap.mapWidth = (int)randomSize;
        CreateMap.mapHeight = (int)randomSize;

        // Reset player attack damage.
        PlayerAttack.damageDealt = 25;
        PlayerAttack.bombDamageDealt = 35;

        LoadScene("Game");
    }

    // Load the game scene with a desert biome.
    public void PlayDesertBiome()
    {
        // Set chosen level, number of obstacles, and size of the map.
        CreateMap.chosenLevel = CreateMap.levelTypes.Desert;
        CreateMap.obstaclesToSpawn = Random.Range(desertMinObstaclesAmount, desertMaxObstaclesAmount);

        // Since map has a separate width and height, use the same number for both.
        // Maybe change this to do separate width and height for more variance.
        float randomSize = Random.Range(desertMinMapSize, desertMaxMapSize);
        CreateMap.mapWidth = (int)randomSize;
        CreateMap.mapHeight = (int)randomSize;

        // Reset player attack damage.
        PlayerAttack.damageDealt = 25;
        PlayerAttack.bombDamageDealt = 35;

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
