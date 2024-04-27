using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;
    public float rotation;
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);

        // Destroy the bullet after a few seconds.
        Destroy(this.gameObject, 5.0f);
    }
    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If bullet enters the player's trigger, deal damage to the player and destroy the bullet.
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehavior>().TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
