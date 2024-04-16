using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Variables related to player movement.
    private Rigidbody2D rb2d;
    private float playerSpeed = 7.0f;
    private float horizontalMovement; // Used for both movement and sprite change.
    private float verticalMovement; // Used for both movement and sprite change.

    // Variables related to player sprite change.
    private float inputAmount = 0.02f;
    private Animator playerAnimator;

    // Variables used for player class and color.
    private Color classColor;
    private string className;

    // Reference to UI script.
    private TempPlayerScoreAndHealth tP;

    // Variables used for weapon behavior.
    private Transform playerTransform;
    private Vector2 lastFacingDirection = Vector2.right;
    private float playerAttackCooldown = 1.1f;
    private float lastAttackTime;
    public GameObject playerWeapon;
    public GameObject playerMelee;
    public GameObject playerBomb;
    private float playerAttackSpeed = 10.5f;

    // Create a list of the various classes that the player can be.
    [HideInInspector] public List<string> playerClassName = new List<string>()
    {
        "archer",
        "wizard",
        "blueberry"
    };

    void Start()
    {
        // Set reference to script.
        tP = GameObject.Find("TempUIHolder").GetComponent<TempPlayerScoreAndHealth>();

        // Set references to player components.
        rb2d = GetComponent<Rigidbody2D>(); // Set rigidbody reference.
        playerAnimator = GetComponent<Animator>(); // Set animator reference.
        playerTransform = transform; // Set transform reference.

        // Assign movement axis references.
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        // Function that gives the player a random class, but will probably change in the future for player to choose their class.
        PlayerClass();
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

        if (Input.GetKey(KeyCode.Space))
        {
            SpawnPlayerWeapon();
        }
        else if (Input.GetKey(KeyCode.Z))
        {

        }
    }

    void PlayerMovement()
    {
        // Change rigidbody velocity (allow player to move) and do it in fixed update for accurate physics interactions.
        rb2d.velocity = new Vector2(horizontalMovement * playerSpeed, verticalMovement * playerSpeed);

        // Run function that changes the player's sprite based on movement.
        UpdateSpriteAnimation();

        // Update the last facing direction whenever movement occurs.
        if (Mathf.Abs(horizontalMovement) > 0.05f || Mathf.Abs(verticalMovement) > 0.05f)
        {
            lastFacingDirection = new Vector2(horizontalMovement, verticalMovement).normalized;
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Used");
        // If player collides with tag Enemy, then run the take damage function.
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage();
           /* StartCoroutine(ConstantDamage());*/
        }
        else if (collision.CompareTag("Projectile"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }
    
    // Temporary coroutine that changes the player's color when they collide with an enemy.
    IEnumerator ColorChange()
    {
        // Change player color to red, wait a set amount of time, then change color back to white.
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Renderer>().material.color = classColor;
    }

    // Function that handles the damage the player takes. 
    public void TakeDamage()
    {
        tP.ChangeLives(-1);
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
   /* IEnumerator ConstantDamage()
    {
        TakeDamage();
        yield return new WaitForSeconds(1);
    }*/

    // Function that sets the player's class.
    // Will probably change as I think player will have the ability to choose their class.
    void PlayerClass()
    {
        // Create a random number that uses the length of the list as the max value.
        float randomNum = Random.Range(0, playerClassName.Count);

        // Set the player's className to an int version of the random number variable. 
        // Use (int) just in case number haapens to be float.
        className = playerClassName[(int) randomNum];
        
        // Display the player's class in the console window.
        Debug.Log($"Your class is: {className}");

        // Use a switch statement to change the player's color and speed based on their class.
        switch (className)
        {
            case "archer":
                // Player is archer, so use base color and make them quick.
                classColor = Color.white;
                playerSpeed = 7.0f;
                break;
            case "wizard":
                // Player is wizard, so make them cyan since it stands out and slow them down.
                classColor = Color.cyan;
                playerSpeed = 5.5f;
                break;
            case "blueberry":
                // Player is blueberry so make them blue and zoomin.
                classColor = Color.blue;
                playerSpeed = 8.5f;
                break;
            default:
                // If none of the classes above, use base color and base speed.
                classColor = Color.white;
                break;
        }
        
        // Once classColor is set, apply it.
        this.GetComponent<Renderer>().material.color = classColor;
    }

    void SpawnPlayerWeapon()
    {
        // Check to see if enough time has passed since the last weapon spawn to spawn another.
        if (Time.time - lastAttackTime < playerAttackCooldown)
        {
            return; // Not enough time has passed, so exit the function.
        }

        // Spawn the attack at the player's position and give it a variable name.
        GameObject playerAttack = Instantiate(playerWeapon, playerTransform.position, Quaternion.identity);
        // Get the rigidbody of the player's attack.
        Rigidbody2D playerAttackRb = playerAttack.GetComponent<Rigidbody2D>();

        // As long as the playerAttack's rigidbody exits (does not equal null), run code below.
        if (playerAttackRb != null)
        {
            // Set the direction the attack moves in the direction the player is facing.
            playerAttackRb.velocity = lastFacingDirection * playerAttackSpeed;

            // Calculate the angle based on the movement direction of the player.
            // Once calculated, set the player's attack to that rotation.
            float angle = Mathf.Atan2(lastFacingDirection.x, -lastFacingDirection.y) * Mathf.Rad2Deg;
            playerAttack.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        //else
        //{
        //    Debug.LogWarning("Rigidbody2D not found on player attack."); // Debug here in case issue occurs.
        //}

        lastAttackTime = Time.time; // Begin attack cooldown.       
    }
    void PlayerBomb()
    {
        if (Time.time - lastAttackTime < playerAttackCooldown)
        {
            return; // Not enough time has passed, so exit the function.
        }
        // Spawn the attack at the player's position and give it a variable name.
        GameObject playerBomb = Instantiate(playerWeapon, playerTransform.position, Quaternion.identity);
        // Get the rigidbody of the player's attack.
        Rigidbody2D playerBombRb = playerBomb.GetComponent<Rigidbody2D>();
    }

    // Function is called when the weapon is destroyed. This then resets the cooldown timer, allowing the player to attack again.
    public void WeaponDestroyed()
    {
        lastAttackTime = Time.time - playerAttackCooldown;
    }
}