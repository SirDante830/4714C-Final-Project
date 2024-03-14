using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
 public GameObject PlayerPos;
 public Transform p1;
 
 public Animator FlyingDemonSprite;
 float speed = .2f;
 float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
     
    //  FlyingDemonSprite.SetFloat("Horizontal", transform.position.x);
    //  FlyingDemonSprite.SetFloat("Speed", transform.position.sqrMagnitude);
    }
    private void FixedUpdate()
     {
      
    distance = Vector2.Distance(transform.position, PlayerPos.transform.position);
      Vector2 direction = PlayerPos.transform.position - transform.position;
      direction.Normalize();
    //  float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;

       transform.position = Vector2.MoveTowards(this.transform.position, PlayerPos.transform.position, speed* Time.deltaTime );
      // transform.LookAt(p1, Vector3.right * -1);
      // transform.rotation = quaternion.Euler(Vector3.forward * angle);
     
    }
}
