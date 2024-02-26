using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMapandType : MonoBehaviour
{
    //The int will be measured as pixels
    //public int lengthOfMap = 10;
    //public int heightOfMap = 10;

    private Camera cam;
    private Color backgroundColor;
    private string typeSelection;
    

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

    // Start is called before the first frame update
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
                cam.backgroundColor = new Color(0f, 0.55f, 0.2f, 1f);
                Debug.Log("Make Grass");
                break;
            case ("Desert"):
                cam.backgroundColor = new Color(0.8f, 0.8f, 0.5f, 1f);
                Debug.Log("Make Desert");
                break;
            case ("Snow"):
                cam.backgroundColor = new Color(0.8f, 0.95f, 0.95f, 1f);
                Debug.Log("Make Snow");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
