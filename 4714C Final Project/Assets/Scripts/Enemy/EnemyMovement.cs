using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyScript
{
    private Vector3 direction;
    private Vector3 velocity;
    public GameObject projectile;
    private float timer;
    public float cooldown = 3f;
    //if you want an enemy to shoot up close or far away etc. Default is 6
    public float rangeDistance = 6;
    //assign the player to this variable
    public GameObject player;

    //you can adjust the speed of the enemy to make speedy or slow enemies
    public static float speed = 1f;

    //determines if enemy is ranged or not
    public bool ranged = false;
    public Animator FlyingDemonSprite;

     void Start()
     {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = cooldown;
     }

    void Update()
    {
        
        //points enemy to player
        direction = player.transform.position - this.transform.position;
        //the velocity that the enemy moves towards player
        velocity = direction.normalized * speed * Time.deltaTime;
        //if enemy is ranged, it will stay a distance away from the player to shoot
        if(ranged && direction.magnitude > rangeDistance)
        {
            // Since the enemy is a certain range away from the player, move towards the player until too close.
            this.transform.position = this.transform.position + velocity;

            // If the timer is at or less than 0, spawn a projectile and restart the countdown.
            if(timer <= 0)
            {
                // Only spawn a projectile if the object is not the flying demon as the flying demon has their own projetile script.
                if (this.gameObject.name != "flyingdemon")
                {
                    Instantiate(projectile, transform.position, transform.rotation);
                }

                timer = cooldown;
            }
            timer -= Time.deltaTime;
          /*  else if(direction.magnitude < rangeDistance)// out of range
            {
                direction = Vector3.zero;
            }*/
        }
        else if(!ranged)
        {
            //moves enemy towards player
            this.transform.position = this.transform.position + velocity;

            // Since the enemy is not ranged, if they are within the range distance, they can now attack, so wait until timer is 0 to attack.
            if (direction.magnitude < rangeDistance) 
            {
                // If timer is at or below 0, attack and restart the countdown.
                if (timer <= 0)
                {
                    // Only spawn a projectile if the object is not the flying demon as the flying demon has their own projetile script.
                    if (this.gameObject.name != "flyingdemon")
                    {
                        Instantiate(projectile, transform.position, transform.rotation);
                    }

                    timer = cooldown;
                }
                timer -= Time.deltaTime;
            }

        }

        // Check to make sure that the sprite reference exists before setting the float to ensure there is no error.
        if (FlyingDemonSprite != null)
        {
            FlyingDemonSprite.SetFloat("Horizontal", direction.x);
            FlyingDemonSprite.SetFloat("Speed", direction.sqrMagnitude);
        }
    }
}
