using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        Debug.Log(levelSystem.GetCurrentLevel());
        levelSystem.AddExperience(50);
        Debug.Log(levelSystem.GetCurrentLevel());
        levelSystem.AddExperience(50);
        Debug.Log(levelSystem.GetCurrentLevel());
    }
}
