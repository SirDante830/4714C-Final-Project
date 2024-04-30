using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    [SerializeField] private LevelBar levelBar;

    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();

        levelBar.SetLevelSystem(levelSystem);
    }
}
