using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class CreateMap : MonoBehaviour
{
    //The int will be measured as pixels
    //public int lengthOfMap = 10;
    //public int heightOfMap = 10;
    //private Camera cam;
    //private Color backgroundColor;
    //private string typeSelection;
    //public static float mapSize;
    //private Color mapColor;

    public static int obstaclesToSpawn = 40;

    // Determine the size of the map.
    public static int mapWidth = 50;
    public static int mapHeight = 50;
    private float tileSize = 2.0f;

    [Header("Biome Tiles")]
    // Sprites that are used for each biome.
    [SerializeField] private Sprite grassSprite;
    [SerializeField] private Sprite desertSprite;
    [SerializeField] private Sprite snowSprite;

    // Objects that contain the look of the background.
    [SerializeField] private GameObject tilePrefab;
    private GameObject background;


    [Header("Border")]
    // Thickness of border and housing object for it.
    private float borderThickness = 6.5f;
    private GameObject border;

    // Distance border is from the edges of the map
    // (may change if camera zooms in or out over time as zoomed out camera could see outside the map).
    private float borderEdgeDistance = 10f;
    
    // Border piece game object.
    private GameObject CreateBorderPiece (string name, Vector2 position, Vector2 scale)
    {
        // Create the object with a specified name, add a collider, set the collider to not trigger to ensure collision, 
        // set its position, set its scale, and return the newly screated borderPiece.
        GameObject borderPiece = new GameObject (name);
        BoxCollider2D collider = borderPiece.AddComponent<BoxCollider2D> ();
        collider.isTrigger = false;
        borderPiece.transform.position = position;
        borderPiece.transform.localScale = new Vector3(scale.x, scale.y, 1f);
        return borderPiece;
    }

    public enum levelTypes
    {
        Grassland,
        Desert,
        Snow
    }

    [Header("Level Select")]
    //Allows the user to select their level type in the Unity Editor
    //[SerializeField] private levelTypes levelSelect;

    //This stores the level selected, and it will be used in the switch case statement to establish the map
    [HideInInspector] public static levelTypes chosenLevel;

    //Depending on the enum selected, make the map with length, height, and color it here through if statements or cases
    void Start()
    {
        // Create the border around the map.
        CreateBorder();

        // Create an empty game object called Background to store the tiles.
        background = new GameObject("Background");

        // Create an offset so that the center of the map is the center of the world in Unity (0,0).
        float xOffset = -((mapWidth - 1) * tileSize / 2);
        float yOffset = -((mapHeight - 1) * tileSize / 2);

        // Create the map and obstacles based on which level is chosen.
        switch (chosenLevel)
        {
            case levelTypes.Grassland:
                CreateGrid(grassSprite, xOffset, yOffset);
                GenerateObstacles("Grassland");
                break;
            case levelTypes.Desert:
                CreateGrid(desertSprite, xOffset, yOffset);
                GenerateObstacles("Desert");
                break;
            case levelTypes.Snow:
                CreateGrid(snowSprite, xOffset, yOffset);
                GenerateObstacles("Snow");
                break;
                
        }

        /*cam = GetComponent<Camera>();
        //chosenLevel = levelSelect;
        typeSelection = chosenLevel.ToString();
        Debug.Log(typeSelection);
        
        FindLevelType(typeSelection);*/
    }

    // Creates the border around the map.
    void CreateBorder()
    {
        // Create an empty game object to the store the border in.
        border = new GameObject("Border");

        float halfMapWidth = (mapWidth - borderEdgeDistance) * tileSize / 2;
        float halfMapHeight = (mapHeight - borderEdgeDistance) * tileSize / 2;

        // Create the left border and child it to the border object.
        GameObject leftBorder = CreateBorderPiece("LeftBorder",
            new Vector2(-halfMapWidth - borderThickness / 2, 0f),
            new Vector2(borderThickness, (mapHeight * tileSize) + borderThickness)
            );
        leftBorder.transform.parent = border.transform;

        // Create the right border.
        GameObject rightBorder = CreateBorderPiece("RightBorder",
            new Vector2(halfMapWidth + borderThickness / 2, 0f),
            new Vector2(borderThickness, (mapHeight * tileSize) + borderThickness)
            );
        rightBorder.transform.parent = border.transform;

        // Create the top border.
        GameObject topBorder = CreateBorderPiece("TopBorder",
            new Vector2(0f, halfMapHeight + borderThickness / 2),
            new Vector2((mapWidth * tileSize) + borderThickness, borderThickness)
            );
        topBorder.transform.parent = border.transform;

        // Create the bottom border.
        GameObject bottomBorder = CreateBorderPiece("BottomBorder",
            new Vector2(0f, -halfMapHeight - borderThickness / 2),
            new Vector2((mapWidth * tileSize) + borderThickness, borderThickness)
            );
        bottomBorder.transform.parent = border.transform;
    }

    // Create the tile grid that will serve as the background by spawning the rows and columns.
    void CreateGrid(Sprite tileSprite, float xOffset, float yOffset)
    {
        // Repeat the spawning of columns for each of the tile x positions.
        for (int i = 0; i < mapWidth; i++)
        {
            // Spawn the columns.
            for (int j = 0; j < mapHeight; j++)
            {
                // Spawn the tile and set its parent.
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.parent = background.transform;

                // Find the sprite renderer on the tile and set its sprite to the one inputted when this function was run.
                SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
                renderer.sprite = tileSprite;

                // Set the position of the tile based on its index in the array, size, and offset in the world.
                tile.transform.position = new Vector2((i * tileSize) + xOffset, (j * tileSize) + yOffset);

                // Set the scale of the tile to the tile size chosen with a slight offset to prevent rendering errors
                // where each tile will be fighting to be on top of the other.
                tile.transform.localScale = new Vector2(tileSize - (tileSize * 0.215f), tileSize - (tileSize * 0.215f));
            }
        }
    }

    [Header("Obstacles")]
    //Used to assign certain prefabs into the scene
    [SerializeField] private GameObject grassObs;
    [SerializeField] private GameObject desertObs;
    [SerializeField] private GameObject snowObs;
    private GameObject obstaclePrefab;
    private Color obsColor;
    private float xCoord;
    private float yCoord;
    private float scale;

    //THIS IS CODE FOR GENERATING OBSTACLES, WE NEED TO MAKE THEM UNIQUE TO THE BIOME TYPE
    void GenerateObstacles(string levelType)
    {
        GameObject obstacles = new GameObject("Obstacles");

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
            // Removed the 4.9 as it ended up going way outside the map.
                //4.9f to prevent the sprite image itself from reaching out of the map
            xCoord = UnityEngine.Random.Range(-mapWidth, mapWidth);
            yCoord = UnityEngine.Random.Range(-mapHeight, mapHeight);
            
            //prevents obstacles from spawning on player start
            while((xCoord < 3f && xCoord > -3f) && (yCoord < 3f && yCoord > -3f))
            {
                xCoord = UnityEngine.Random.Range(-mapWidth, mapWidth);
                yCoord = UnityEngine.Random.Range(-mapHeight, mapHeight);
            }
            //this is not used, I'm keeping it here just in case.
            scale = UnityEngine.Random.Range(0.5f, 1.5f);

            //Creates obstacle and assigns it in world through x and y coordinates. -0.01f for z-axis in case of layer issues
            GameObject newObstacle = Instantiate(obstaclePrefab, new Vector3(xCoord, yCoord, -0.01f), transform.rotation);
            newObstacle.transform.parent = obstacles.transform;
        }
    }


    /*
* No longer being used as new map spawn exists now.

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

    */

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

}
