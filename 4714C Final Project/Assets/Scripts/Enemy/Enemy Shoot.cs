using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject player;
    private Vector3 direction;
    public float projectileSpeed = 1f;

    // Projectile rigidbody

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = player.transform.position - this.transform.position;
        Destroy(this.gameObject, 4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Time.deltaTime * projectileSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehavior>().TakeDamage();
        }
        // Destroy(gameObject);
    }
}
