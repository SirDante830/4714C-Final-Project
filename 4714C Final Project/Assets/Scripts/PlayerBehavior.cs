using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

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
    private float colorChangeTime = 0.25f; // Time between changes of color when player takes damage.

    // Variables used for weapon behavior.
    private Transform playerTransform;
    private Vector2 lastFacingDirection = Vector2.right;
    private float playerAttackCooldown = 1.1f;
    private float lastAttackTime;
    public GameObject playerWeapon;
    public GameObject playerMelee;
    public GameObject playerBomb;
    private float playerAttackSpeed = 10.5f;
    private float bombCoolDown = 5.0f;

    // Create a list of the various classes that the player can be.
    [HideInInspector] public List<string> playerClassName = new List<string>()
    {
        "archer",
        "wizard",
        "blueberry"
    };

    // Variables for lives and score.
    private int maxLives = 20;
    private int _lives = 0;
    private int bombs = 3;

    // When hit by an enemy or enemy attack, this is the amount of lives the player loses.
    private int livesLostOnHit = -1;

    // Lives variable that is accessed in other classes, so _lives is not accessed by other classes.
    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
        }
    }

    private int _score = 0;

    // Score variables that is accessed in other classes, so _score is not accessed by other classes.
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    [Header("UI Elements")]
    // UI variables.
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverDisplay;

    // Delegate and event when game is over.
    public delegate void GameOverEvent();
    public event GameOverEvent gameIsOver;

    void Start()
    {
        // Set references to player components.
        rb2d = GetComponent<Rigidbody2D>(); // Set rigidbody reference.
        playerAnimator = GetComponent<Animator>(); // Set animator reference.
        playerTransform = transform; // Set transform reference.

        // Assign movement axis references.
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        // Function that gives the player a random class, but will probably change in the future for player to choose their class.
        PlayerClass();

        // Set player lives to the max at the start. Do this before setting UI so it is up to date.
        Lives = maxLives;

        // Run at start to make sure UI is displayed when player begins the game.
        SetUI();
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

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z))
        {
            SpawnPlayerWeapon();
        }
       /* else if (Input.GetKey(KeyCode.Z))
        {
            Bomb();
        }*/
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
        // If player collides with tag Enemy, then run the take damage function.
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage();
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
        yield return new WaitForSeconds(colorChangeTime);
        this.GetComponent<Renderer>().material.color = classColor;
        /*yield return new WaitForSeconds(colorChangeTime);
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(colorChangeTime);
        this.GetComponent <Renderer>().material.color = classColor;*/
    }

    // Function that handles the damage the player takes. 
    public void TakeDamage()
    {
        ChangeLives(livesLostOnHit);
        StartCoroutine(ColorChange()); // Run change color coroutine
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
        GameObject playerAttack;
        // Check to see if enough time has passed since the last weapon spawn to spawn another.
        if (Time.time - lastAttackTime < playerAttackCooldown)
        {
            return; // Not enough time has passed, so exit the function.
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            // Spawn the attack at the player's position and give it a variable name.
            playerAttack = Instantiate(playerWeapon, playerTransform.position, Quaternion.identity);
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
            else
            {
                Debug.LogWarning("Rigidbody2D not found on player attack."); // Debug here in case issue occurs.
            }


        }
        else if (Input.GetKey(KeyCode.Z))
        {
            // Spawn the attack at the player's position and give it a variable name.
            playerAttack = Instantiate(playerBomb, playerTransform.position, Quaternion.identity);
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
            else
            {
                Debug.LogWarning("Rigidbody2D not found on player attack."); // Debug here in case issue occurs.
            }
            bombs -= 1;
            Debug.Log($"Bomb: {bombs}");
            if(bombs == 0)
            {
               /* if (Time.time - lastAttackTime < bombCoolDown)
                {
                    return; // Not enough time has passed, so exit the function.
                }*/
                //wait 5 seconds after no bombs, then add a bomb every 3 or so seconds
                StartCoroutine(TimeCoroutine());

            }

        }
        lastAttackTime = Time.time; // Begin attack cooldown.       
    }
   /* void Bomb()
    {
        // Check to see if enough time has passed since the last weapon spawn to spawn another.
        if (Time.time - lastAttackTime < playerAttackCooldown)
        {
            return; // Not enough time has passed, so exit the function.
        }

        // Spawn the attack at the player's position and give it a variable name.
        GameObject playerAttack = Instantiate(playerBomb, playerTransform.position, Quaternion.identity);
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
        else
        {
            Debug.LogWarning("Rigidbody2D not found on player attack."); // Debug here in case issue occurs.
        }

        lastAttackTime = Time.time; // Begin attack cooldown.      
    }*/

    // Function is called when the weapon is destroyed. This then resets the cooldown timer, allowing the player to attack again.
    public void WeaponDestroyed()
    {
        lastAttackTime = Time.time - playerAttackCooldown;
    }

    // Function to make sure lives and score do not go outside their boundaries.
    void MinAndMaxChecks()
    {
        // If lives somehow go over the max, keep them at max.
        if (_lives > maxLives)
        {
            _lives = maxLives;
        }

        // If score somehow goes below 0, set it back to 0.
        if (_score < 0)
        {
            _score = 0;
        }
    }

    // Function to change score.
    public void ChangeScore(int scoreChange)
    {
        // Adjust score by the change amount.
        Score += scoreChange;

        // Run a check to make sure the max or minimum of lives and score are not hit. *Score does not have a max.
        MinAndMaxChecks();

        // Set the UI so it changes when score changes.
        SetUI();
    }

    // Function to change lives.
    public void ChangeLives(int livesChange)
    {
        // Try to change lives.
        try
        {
            // Adjust the lives by the change amount.
            Lives += livesChange;

            // After changing, if the lives are at or below 0, throw an exception made in the OutOfLivesException class.
            if (_lives <= 0)
            {
                throw new OutOfLivesException();
            }
        }
        // If an OutOfLiveException is caught, begin the zero lives remaining coroutine that ends the game and debug that the player is out of lives.
        catch (OutOfLivesException exception)
        {
            StartCoroutine(ZeroLivesRemaining());
            Debug.Log("Can't continue because there are no more lives remaining!" + exception);
        }

        // Run a check to make sure the max or minimum of lives and score are not hit. *Score does not have a max.
        MinAndMaxChecks();

        // Set the UI so it changes when the lives change.
        SetUI();
    }

    // Function to set UI.
    void SetUI()
    {
        // Set the livesText to the text in "" + the current lives variable value.
        livesText.text = "Lives " + _lives;

        // Set the scoreText to the text in "" + the current score variable value.
        scoreText.text = _score + " Score";
    }


    IEnumerator TimeCoroutine()
    {
        yield return new WaitForSeconds(5);
        bombs += 3;

    }
    // Coroutine that starts when the player has no more lives.
    IEnumerator ZeroLivesRemaining()
    {
        // Stop time, turn on the game over display to play its animation
        // (animation update mode is set to unscaled time in the inspector to allow it to play while time is stopped),
        // wait until a little bit after the animation is done (use WaitForSecondsRealtime so it uses unscaled time,
        // allowing the wait time to work even if time scale is at 0), then run the event.
        Time.timeScale = 0f;
        gameOverDisplay.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        gameIsOver();
    }

    // Class that creates an exception that is used when there are no more lives remaining.
    public class OutOfLivesException : Exception
    {
        public OutOfLivesException() : base("Player ran out of lives!")
        {

        }
    }
}