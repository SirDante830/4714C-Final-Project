using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make sure this script is assigned to each of the player weapons (the arrow weapon used for each class).
public class PlayerAttack : MonoBehaviour
{
    //public GameObject explosionAnimation; // This needs to be assigned in the inspector of the player weapon. (Not doing).
    private PlayerBehavior pB; // This is a reference to the player behavior script which is used in different parts of the script.
    private int enemyHitScore = 10; // This is the amount of score the player is given when they hit an enemy.
    private int damageDealt = 25; // This is the amount of damage the player's weapon deals to enemies.

    // Reference to UI script.
    private TempPlayerScoreAndHealth tP;

    void Start()
    {
        // Assign the reference to the player script.
        pB = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();

        // Set reference to script.
        tP = GameObject.Find("TempUIHolder").GetComponent<TempPlayerScoreAndHealth>();

        // Destroy the player attack after a set amount of time so it does not exist for too long.
        Destroy(this.gameObject, 4.5f);
    }

    // When the weapon collides with something, check the tag to see what it is. 
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            // If the arrow hits an enemy, deal damage to the enemy, change the player's score, and destroy the arrow.
            // Right before destroying, call player weapon destroyed function to allow the player to attack again.
            hit.GetComponent<EnemyScript>().EnemyTakeDamage(damageDealt);
            tP.ChangeScore(enemyHitScore);
            pB.WeaponDestroyed();
            Destroy(this.gameObject);
        }
        else if (hit.CompareTag("Projectile")){
            Destroy(hit.gameObject);
        }
    }
}