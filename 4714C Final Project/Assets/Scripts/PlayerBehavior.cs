using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Variables related to player movement.
    private Rigidbody2D rb2d;
    private float playerSpeed = 9.0f;
    private float horizontalMovement; // Used for both movement and sprite change.
    private float verticalMovement; // Used for both movement and sprite change.

    // Variables related to player sprite change.
    private float inputAmount = 0.02f;
    private Animator playerAnimator;

    void Start()
    {
        // Set references to player components.
        rb2d = GetComponent<Rigidbody2D>(); // Set rigidbody reference.
        playerAnimator = GetComponent<Animator>(); // Set animator reference.

        // Assign movement axis references.
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
    }

    void Update()
    {
        // Run PlayerInput function that checks for any input made by the user.
        PlayerInput();
    }

    void FixedUpdate()
    {
        // Run function that controls the player's movement in fixed update for accurate physics interactions.
        PlayerMovement();
    }

    // Function to check for player input.
    void PlayerInput()
    {
        // Assign movement axis references.
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
    }

    void PlayerMovement()
    {
        // Change rigidbody velocity (allow player to move) and do it in fixed update for accurate physics interactions.
        rb2d.velocity = new Vector2(horizontalMovement * playerSpeed, verticalMovement * playerSpeed);

        // Run function that changes the player's sprite based on movement.
        UpdateSpriteAnimation();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If player collides with tag Enemy, then run the take damage function.
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
    
    // Temporary coroutine that changes the player's color when they collide with an enemy.
    IEnumerator ColorChange()
    {
        // Change player color to red, wait a set amount of time, then change color back to white.
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    // Function that handles the damage the player takes. 
    public void TakeDamage()
    {
        StartCoroutine(ColorChange()); // Run change color coroutine
        Debug.Log("Damage Taken"); // Send a message in the console.
    }

    // Updates the sprite animation based on the direction the player is moving in.
    void UpdateSpriteAnimation()
    {
        // Based on the direction of movement, set the sprite for that direction.
        if (horizontalMovement < -inputAmount)
        {
            // Moving left, set movingLeft to true and others to false.
            SetAnimatorBools(true, false, false, false);
        }
        else if (horizontalMovement > inputAmount)
        {
            // Moving right, set movingRight to true and others to false.
            SetAnimatorBools(false, true, false, false);
        }
        else if (verticalMovement > inputAmount)
        {
            // Moving up, set movingUp to true and others to false.
            SetAnimatorBools(false, false, true, false);
        }
        else if (verticalMovement < -inputAmount)
        {
            // Moving down, set movingDown to true and others to false.
            SetAnimatorBools(false, false, false, true);
        }
    }

    // Updates the Animator bools. Makes code look a lot better as the alternative was listing what is in this function in each if statement above.
    void SetAnimatorBools(bool movingLeft, bool movingRight, bool movingUp, bool movingDown)
    {
        playerAnimator.SetBool("moveLeft", movingLeft);
        playerAnimator.SetBool("moveRight", movingRight);
        playerAnimator.SetBool("moveUp", movingUp);
        playerAnimator.SetBool("moveDown", movingDown);
    }
}