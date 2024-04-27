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
}
