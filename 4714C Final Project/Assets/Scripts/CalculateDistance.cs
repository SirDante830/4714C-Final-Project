using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDistance : MonoBehaviour
{
   
    public GameObject enemydistance;
    
    
    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
          DistanceCalc();
          AngleCalc();
        }
    }
    void damageit()
    {

    }
   private  void DistanceCalc()// different ways to get the distance magnitude between 2 objects
    {
        //++++++++++ finding distance method1: Pythagorus' theorem +++++++++
     float distance = Mathf.Sqrt(Mathf.Pow(enemydistance.transform.position.x -transform.position.x,2)+
                                Mathf.Pow(enemydistance.transform.position.y- transform.position.y,2));

      //+++++++++finding distance method2: Unity Distance +++++++++
     Vector3  EnemyPos = new Vector3(enemydistance.transform.position.x,enemydistance.transform.position.y,0);
     Vector3  Playerpos = new Vector3(transform.position.x,transform.position.y,0);  
     float UnityDistance=  Vector3.Distance(EnemyPos,Playerpos);

     //+++++++ finding distance method3: magnitude +++++++++
     Vector3 PlayerToEnemy = Playerpos - EnemyPos;

     Debug.Log("Distance: "+ distance);
     Debug.Log("Unity Distance: "+ UnityDistance);
     Debug.Log("vector magnitude: "+ PlayerToEnemy.magnitude);
    }

    private void AngleCalc()
    {
        Vector3 PlayerFoward = transform.forward;
        Vector3 EnemyDirection = enemydistance.transform.position - transform.position;
        Debug.DrawRay(this.transform.position, PlayerFoward*3, Color.red,3);
        Debug.DrawRay(this.transform.position, EnemyDirection, Color.blue,3);
    }
}
