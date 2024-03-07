using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class CreateMapandType : MonoBehaviour
{
    //The int will be measured as pixels
    //public int lengthOfMap = 10;
    //public int heightOfMap = 10;
    public int obstaclesToSpawn = 10;
    private Camera cam;
    private Color backgroundColor;
    private string typeSelection;
    public float mapSize;
    private Color mapColor;

    public enum levelTypes
    {
        Grassland,
        Desert,
        Snow
    }

    //Allows the user to select their level type in the Unity Editor
    [SerializeField] public levelTypes levelSelect;
    //This stores the level selected, and it will be used in the switch case statement to establish the map
    private levelTypes chosenLevel;

    //Depending on the enum selected, make the map with length, height, and color it here through if statements or cases
    void Start()
    {
        cam = GetComponent<Camera>();
        chosenLevel = levelSelect;
        typeSelection = chosenLevel.ToString();
        Debug.Log(typeSelection);
        
        FindLevelType(typeSelection);
    }

    void FindLevelType(string typeSelection)
    {
        switch(typeSelection)
        {
            //instantiate the map here
            //Actually gives the background color to main camera to simulate a map
            case ("Grassland"):
                mapColor = new Color(0f, 0.55f, 0.2f, 1f);
                Debug.Log("Make Grass");
                CreateGrassland(mapColor);
                break;
            case ("Desert"):
                mapColor = new Color(0.8f, 0.8f, 0.5f, 1f);
                Debug.Log("Make Desert");
                CreateDesert(mapColor);
                break;
            case ("Snow"):
                mapColor = new Color(0.8f, 0.95f, 0.95f, 1f);
                Debug.Log("Make Snow");
                CreateSnow(mapColor);
                break;
        }
    }

    void CreateGrassland(Color c)
    {
        GenerateObstacles("Grassland");
        Color terrainColor = c;
        GameObject Ground;
        Ground = new GameObject("Ground");
        SpriteRenderer spriteRenderer = Ground.AddComponent<SpriteRenderer>();
        spriteRenderer.color = c;
        spriteRenderer.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        spriteRenderer.transform.localScale = new Vector3(mapSize * 1000f, mapSize * 1000f, 1.0f);
        Ground.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void CreateDesert(Color c)
    {
        GenerateObstacles("Desert");
        Color terrainColor = c;
        GameObject Ground;
        Ground = new GameObject("Ground");
        SpriteRenderer spriteRenderer = Ground.AddComponent<SpriteRenderer>();
        spriteRenderer.color = c;
        spriteRenderer.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        spriteRenderer.transform.localScale = new Vector3(mapSize * 1000f, mapSize * 1000f, 1.0f);
        Ground.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void CreateSnow(Color c)
    {
        GenerateObstacles("Snow");
        Color terrainColor = c;
        GameObject Ground;
        Ground = new GameObject("Ground");
        SpriteRenderer spriteRenderer = Ground.AddComponent<SpriteRenderer>();
        spriteRenderer.color = c;
        spriteRenderer.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        spriteRenderer.transform.localScale = new Vector3(mapSize * 1000f, mapSize * 1000f, 1.0f);
        Ground.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    /*
       //THIS IS CODE FOR GENERATING OBSTACLES, WE NEED TO MAKE THEM UNIQUE TO THE BIOME TYPE
    for (int obstacleSpawned = 0; obstacleSpawned < obstaclesToSpawn; obstacleSpawned++)
        {
            xcount = Random.Range(-8f, 0f);
            zcount = Random.Range(-8f, 0f);
            greenColor = Random.Range(0.2f, 1.0f);
            scale = Random.Range(0.5f, 1.5f);

            //This needs to be changed to a prefab for different obstacles, or at least not a 3d shape.
            GameObject Tree = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            Tree.name = "Tree";

            var treeRenderer = Tree.GetComponent<Renderer>();
            Color newColor = new Vector4(0f, greenColor, 0f);
            treeRenderer.material.SetColor("_Color", newColor);
            
            Tree.transform.SetParent(Forest.transform);
        }
    */

    //Used to assign certain prefabs into the scene
    public GameObject obstaclePrefab;
    public GameObject grassObs;
    public GameObject desertObs;
    public GameObject snowObs;
    private GameObject allObstacles;
    private Color obsColor;
    private float xCoord;
    private float yCoord;
    private float scale;
       //THIS IS CODE FOR GENERATING OBSTACLES, WE NEED TO MAKE THEM UNIQUE TO THE BIOME TYPE
       void GenerateObstacles(string levelType)
    {
        switch(levelType)
        {
            case ("Grassland"):
                obstaclePrefab = grassObs;
                break;
            case ("Desert"):
                obstaclePrefab = desertObs;
                break;
            case ("Snow"):
                obstaclePrefab = snowObs;
                break;
        }
        for (int obstacleSpawned = 0; obstacleSpawned < obstaclesToSpawn; obstacleSpawned++)
        {
            //Random values for x and y coordinates, only within the map boundaries
            //4.9f to prevent the sprite image itself from reaching out of the map
            xCoord = UnityEngine.Random.Range(-mapSize * 4.9f, mapSize * 4.9f);
            yCoord = UnityEngine.Random.Range(-mapSize * 4.9f, mapSize * 4.9f);
            
            //prevents obstacles from spawning on player start
            while((xCoord < 3f && xCoord > -3f) && (yCoord < 3f && yCoord > -3f))
            {
                xCoord = UnityEngine.Random.Range(-mapSize * 4.9f, mapSize * 4.9f);
                yCoord = UnityEngine.Random.Range(-mapSize * 4.9f, mapSize * 4.9f);
            }
            //this is not used, I'm keeping it here just in case.
            scale = UnityEngine.Random.Range(0.5f, 1.5f);

            //Creates obstacle and assigns it in world through x and y coordinates. -0.01f for z-axis in case of layer issues
            Instantiate(obstaclePrefab, new Vector3(xCoord, yCoord, -0.01f), transform.rotation);
            

            //set to obstacle parent
            //obstaclePrefab.transform.SetParent(allObstacles.transform);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
