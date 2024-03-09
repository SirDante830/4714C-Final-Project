using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 direction;
    private Vector3 velocity;

    //if you want an enemy to shoot up close or far away etc. Default is 6
    public float rangeDistance = 6;
    //assign the player to this variable
    public GameObject player;

    //you can adjust the speed of the enemy to make speedy or slow enemies
    public float speed = 1f;

    //determines if enemy is ranged or not
    public bool ranged = false;

    // Update is called once per frame
    void Update()
    {
        //points enemy to player
        direction = player.transform.position - this.transform.position;
        //the velocity that the enemy moves towards player
        velocity = direction.normalized * speed * Time.deltaTime;
        //if enemy is ranged, it will stay a distance away from the player to shoot
        if(ranged)
        {
            if(direction.magnitude > rangeDistance)
            {
                this.transform.position = this.transform.position + velocity;
            }
        }
        else
        {
            //moves enemy towards player
            this.transform.position = this.transform.position + velocity;
        }
    }
}
