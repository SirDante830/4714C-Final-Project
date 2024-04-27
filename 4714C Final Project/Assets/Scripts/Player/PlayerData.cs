using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //public int level;
    public int lives;
    public int score;
    public float[] position;
    public string playerClass;
    public CreateMap.levelTypes biome;
    public int mapWidth;
    public int mapHeight;

    public PlayerData (PlayerBehavior player)
    {
        //level = player.level;
        lives = player.Lives;
        score = player.Score;

        // Store the vector 3 into an array of floats for each position.
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        // Save the player class.
        playerClass = player.className;

        //// Save map data.
        //biome = CreateMap.chosenLevel;
        //mapWidth = CreateMap.mapWidth;
        //mapHeight = CreateMap.mapHeight;
    }
}
