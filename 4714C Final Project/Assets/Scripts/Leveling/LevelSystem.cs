using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    private int level;
    private int experience;
    private int experienceToNextLevel;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }

    // Add the amount of experience by the inputted amount when the function is called.
    public void AddExperience(int amount)
    {
        experience += amount;

        // If the experience is more than needed for the next level, level the player up and reduce the amount of reamining experience.
        if (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
        }
    }

    public int GetCurrentLevel()
    {
        return level;
    }
}
