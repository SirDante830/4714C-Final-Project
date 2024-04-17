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
    public float speed = 1f;

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
        if(ranged)
        {
            if(direction.magnitude > rangeDistance)// while in range
            {
                this.transform.position = this.transform.position + velocity;

            }
            if(timer <= 0)
            {
                Instantiate(projectile, transform.position, transform.rotation);
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
          //  direction = Vector3.zero;
                this.transform.position = this.transform.position + velocity;

        }
        FlyingDemonSprite.SetFloat("Horizontal", direction.x);
        FlyingDemonSprite.SetFloat("Speed", direction.sqrMagnitude);
    }
}
