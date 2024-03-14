using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreateMapandType;

public class EnemyScript : MonoBehaviour
{
    public static int EnemyHP;
    public static int  EnemyDmg;
    public static float EnemySpeed;
    private Color classColor;
    private string className;
    private string EnemySelection;
    [SerializeField] private GameObject enemy;
    public enum EnemyTypes
    {
        Crawler,
        Speeder,
        Shooter
    }
    private EnemyTypes EnemySelect;
    public EnemyTypes ChosenEnemy;
    // Start is called before the first frame update
    void Start()
    {
        ChosenEnemy = EnemySelect;
        EnemySelection = ChosenEnemy.ToString();
        EnemyClass(EnemySelection);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EnemyClass(string EnemySelection)
    {
        switch (EnemySelection)
        {
            case "Crawler":
                Debug.Log($"{EnemySelection}");
                EnemyHP = 100;
                EnemySpeed = 1.5f;
                break;
            case "Speeder":
                EnemyClass(EnemySelection);
                EnemyHP = 150;
                EnemySpeed = 2.5f;
                break;
            case "Shooter":
                EnemyClass(EnemySelection);
                EnemyHP = 200;
                EnemySpeed = 1.5f;
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D Hit)
    {
        if (Hit.CompareTag("Arrow"))
        {
            Debug.Log("hit");
            EnemyHP -= 50;
        }
        if(EnemyHP <= 0)
        {
            Debug.Log("Destroyed");
            Destroy(enemy);
        }
    }
}
