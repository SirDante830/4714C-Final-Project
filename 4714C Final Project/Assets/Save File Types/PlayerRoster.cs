using System.IO;
using UnityEngine;

public class PlayerRoster : MonoBehaviour
{
[System.Serializable]public class player //making values for the player class
  {
   public string name;
   public int health;
   public int damage;
  } 
   
 [System.Serializable]public class PlayerList // making an array list of players
  {
    public player[] players;
  } 
  public PlayerList playerRoster = new PlayerList();// player list array now has 3 element values 
    void Start()
    {
       string filePath = Path.Combine(Application.dataPath, "Save File Types", "Playerdata.json");// make path
       string JsonData = File.ReadAllText(filePath); //locate all files named "Playerdata.json"
        playerRoster = JsonUtility.FromJson<PlayerList>(JsonData);// grabbing the array and assigning the json strings
        
        foreach(var playerRoles in playerRoster.players)// checking for every element of players in the json file
        {
            Debug.LogFormat("Name: {0}, Health: {1}, Damage: {2}", playerRoles.name,playerRoles.health, playerRoles.damage);
            
        }
    }
}
