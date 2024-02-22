using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    public float inputAmount = 0.2f;
    public GameObject square;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        if (horizontalMovement < 0)
        {
            //GetComponentInChildren<Transform>().
            this.transform.Rotate(0f, 180f, 0f);
        }
        else if (horizontalMovement > 0)
        {
            this.transform.Rotate(0f, 0f, 0f);
        }


    }
    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        rb2d.velocity = new Vector2 (horizontalMovement * speed, verticalMovement * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If player collides with tag enemy, then color changes to red
        if (collision.CompareTag("enemy"))
        {
            
            StartCoroutine(ColorChange());
            Debug.Log("Damage Taken");

        }
    }
    IEnumerator ColorChange()
    {
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Renderer>().material.color = Color.white;
    }

}
