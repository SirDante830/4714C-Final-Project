using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
 public GameObject PlayerPos;
 Vector3 direction;
 float speed = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate() 
    {
      direction = PlayerPos.transform.position -this.transform.position;
      Vector3 velocity = direction.normalized * speed* Time.deltaTime;
      this.transform.position = this.transform.position + velocity ; 
    }
}
