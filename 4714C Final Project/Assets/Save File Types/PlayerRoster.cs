using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerRoster : MonoBehaviour
{
   public TextAsset playerdataJson;// attaching the playerdata.json file

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
        playerRoster = JsonUtility.FromJson<PlayerList>(playerdataJson.text);// grabbing the array and assigning the json strings
        
        foreach(var playerRoles in playerRoster.players)// checking for every element of players in the json file
        {
            Debug.LogFormat("Name: {0}, Health: {1}, Damage: {2}", playerRoles.name,playerRoles.health, playerRoles.damage);
        }
    }
}
