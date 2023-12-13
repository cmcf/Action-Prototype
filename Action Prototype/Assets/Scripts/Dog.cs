using Abertay.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dog : MonoBehaviour
{
    [SerializeField] float defaultMoveSpeed = 5f;
    [SerializeField] float currentMoveSpeed; // Default movement speed
    [SerializeField] float sprintSpeed = 10f; // Speed when sprinting
    [SerializeField]float stamina = 80f;      // Maximum stamina
    [SerializeField] float staminaDepletionRate = 10f;
    [SerializeField] float staminaRegainRate = 20f;
    [SerializeField] float amplitude = 1f;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float projectileSpeed = 5f;

    float airSpeed = 5f;
    float defaultSprintSpeed = 8f;
     public float currentStamina;

    public Slider staminaSlider;
    public AudioClip bounceSFX;
    public AudioClip barkSFX;

    Rigidbody2D rb;
    Animator animator;
    [SerializeField] GameObject projectilePrefab;


    public Transform attackPoint;
    public LayerMask enemyLayers;
    public Image bulletImage;

    Vector3 initialPosition;

    [SerializeField] Vector2 moveInput;

    bool isSprinting = false;
    bool isGrounded = true;
    public bool isAttacking = false;
    public bool canMoveDog = false;

  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentStamina = stamina;
        currentMoveSpeed = defaultMoveSpeed;
        initialPosition = transform.position;
        staminaSlider.value = currentStamina;
    }

    void FixedUpdate()
    {
        Walk();
        FlipSprite();
    }

    void Update()
    {
        StaminaManagement();
        UpdateDogUI();
        Die();
    }

    void UpdateDogUI()
    {
        // Shows player when they can fire bullet
        if (!isAttacking)
        {
            bulletImage.color = Color.white;
        }
        else
        {
            bulletImage.color = Color.grey;
        }
        staminaSlider.value = currentStamina;    
    }

    public void DisableInput()
    {
        moveInput = Vector2.zero;
    }
    void OnQuit(InputValue value)
    {
        // Quits the game when player presses ESC
        Application.Quit();
    }

    void StaminaManagement()
    {
        if (isSprinting)
        {
            // Stamina reduces when dog is sprinting
            currentStamina -= staminaDepletionRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, stamina);

            // Dog stops sprinting when stamina runs out
            if (currentStamina <= 0)
            {
                StopSprinting();
            }
        }
        else
        {
            // Regain stamina when not sprinting
            currentStamina += staminaRegainRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, stamina);
        }
        if (!isGrounded)
        {
            sprintSpeed = airSpeed;
        }
        else
        {
            sprintSpeed = defaultSprintSpeed;
        }
    }

    void OnMove(InputValue value)
    {

        // Gets value of dog movement and true if greater than 0 
        bool dogHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        // Flips sprite by scaling the x axis when dog has horizontal speed
        if (dogHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }

        // Get input values from dog
        moveInput = value.Get<Vector2>();

        // Apply movement to the dog
        Vector2 dogVelocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
        rb.velocity = dogVelocity;
    }

    void Walk()
    {
        // X velocity is multiplied by the move speed and retains current velocity on the y axis
        Vector2 playerVelocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        // Checks if player is moving left or right
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        // Sets animation state to run
        animator.SetBool("isWalking", playerHasHorizontalSpeed);
    }

    void OnSprint(InputValue value)
    {
        // Checks if the sprint button is held down and if the dog has enough stamina and is on the ground
        if (value.isPressed && currentStamina > 0)
        {
            Sprint();
            
        }
        else
        {
            StopSprinting();
        }   
    }

    void Sprint()
    {
        // Movement speed is increased
        isSprinting = true;
        currentMoveSpeed = sprintSpeed;
        LogStaminaUse();
    }

     void LogStaminaUse()
     {
        // Creates heat map for sprint locations
        Color c = Color.blue;
        c.a = 0.4f;
        AnalyticsManager.LogHeatmapEvent("StaminaLocation", transform.position, c);
        Debug.Log(transform.position);
     }
    
     void OnFire()
     { 
        if (!isAttacking)
        {
            isAttacking = true;
            // Instantiate a new bullet from the bullet prefab
            GameObject newBullet = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            animator.SetBool("isAttacking", true);

            // Calculate the bullet's direction based on the player's scale
            float direction = transform.localScale.x > 0 ? 1f : -1f;

            // Get the sprite renderer of the bullet
            SpriteRenderer bulletSpriteRenderer = newBullet.GetComponent<SpriteRenderer>();

            // Flip the bullet sprite by adjusting its local scale
            Vector3 newScale = bulletSpriteRenderer.transform.localScale;
            newScale.x *= direction;
            bulletSpriteRenderer.transform.localScale = newScale;

            // Set the initial velocity of the bullet to move in the desired direction
            Rigidbody2D bulletRigidbody = newBullet.GetComponent<Rigidbody2D>();
            if (bulletRigidbody != null)
            {
                // Calculate the bullet's velocity based on the player's direction
                Vector2 bulletVelocity = new Vector2(projectileSpeed * direction, 0f);

                // Set the bullet's velocity
                bulletRigidbody.velocity = bulletVelocity;
            }
        }
     }
    public void DogAttack()
    {
        AudioSource.PlayClipAtPoint(barkSFX, Camera.main.transform.position, 0.2f);
        isAttacking = true;
        Invoke("StopAttack", 0.2f);
    }

    void StopAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    void StopSprinting()
    {
        // Movement speed is set back to default
        isSprinting = false;
        currentMoveSpeed = defaultMoveSpeed;
        Debug.Log("Stop sprint: "+ currentStamina);
    }

    void FlipSprite()
    {
        // Gets value of player movement and true if greater than 0.
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        // Flips sprite by scaling the the x axis when player has horizonal speed
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Ink"))
        {
            Ascend();
        }
    }

    void Ascend()
    {
        // Play sound
        AudioSource.PlayClipAtPoint(bounceSFX, Camera.main.transform.position, 0.2f);
        rb.velocity = new Vector2(rb.velocity.x, amplitude);
        isGrounded = false;
    }

    void Die()
    {
        if (!GameSession.Instance.isAlive)
        {
            animator.SetBool("isDead", true);
            currentStamina = stamina;
        }
        else
        {
            animator.SetBool("isDead", false);
        }
    }
}