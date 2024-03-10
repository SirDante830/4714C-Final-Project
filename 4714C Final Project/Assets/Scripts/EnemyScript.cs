using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public static int EnemyHP;
    public static int  EnemyDmg;
    public static float EnemySpeed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Crawler(){
        EnemyHP = 100;
        EnemySpeed = 1.5f;
        

    }
    private void Speeder(){
        EnemyHP = 100;
        EnemySpeed = 2.5f;

    }
    private void Shooter(){
        EnemyHP = 100;
        EnemySpeed = 1.5f;
    }
    private void OnColliderEnter2D(Collider Hit)
    {
        if (Hit.CompareTag("Player"))
        {
            //If the enemy gets hit by the player, then HP is reduced by 50
            // If the arrow hits an enemy, deal damage to the enemy, change the player's score, and destroy the arrow.
            // Right before destroying, call player weapon destroyed function to allow the player to attack again.
            //hit.GetComponent<EnemyBehavior>().EnemyTakeDamage(damageDealt);
            EnemyHP -= 50;
        }
    }
}
