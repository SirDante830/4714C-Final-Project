using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    [SerializeField] private Slider experienceBar;
    [HideInInspector] public LevelSystem levelSystem;

    // Window that pops up giving the player a choice on what upgrade they want.
    [SerializeField] private GameObject levelUpOptions;

    private void Awake()
    {
        //experienceBar = transform.Find("ExperienceBar").GetComponent<Slider>();
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBar.value = experienceNormalized;
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        // Set levelSystem object reference.
        this.levelSystem = levelSystem;

        // Set the size of the experience bar.
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        // Subscribe to events.
        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        // Stop time and give the player a choice on what level up buff they want.
        Time.timeScale = 0.0f;
        levelUpOptions.SetActive(true);
    }

    // Increase the player's attack if they choose the attack buff button.
    public void IncreaseAttack()
    {
        PlayerAttack.damageDealt += 10;
        PlayerAttack.bombDamageDealt += 10;
        LevelUpOptionChosen();
    }

    // Increase the players current lives and their max number of lives.
    public void IncreaseHealth()
    {
        PlayerBehavior.instance.Lives += 10;
        PlayerBehavior.instance.maxLives += 10;
        PlayerBehavior.instance.SetUI();
        LevelUpOptionChosen();
    }

    // Increase the player's speed.
    public void IncreaseSpeed()
    {
        PlayerBehavior.instance.playerSpeed += 1.5f;
        LevelUpOptionChosen();
    }

    // Once the player has chosen a level up option, turn off the options and resume the game.
    private void LevelUpOptionChosen()
    {
        levelUpOptions.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
