using UnityEngine;
using UnityEngine.InputSystem;

public class Dog : MonoBehaviour
{
    [SerializeField] float defaultMoveSpeed = 5f;
    [SerializeField] float currentMoveSpeed; // Default movement speed
    [SerializeField] float sprintSpeed = 10f; // Speed when sprinting
    [SerializeField]float stamina = 80f;      // Maximum stamina
    [SerializeField] float staminaDepletionRate = 10f;
    [SerializeField] float amplitude = 1f;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float projectileSpeed = 5f;

    [SerializeField] float attackRange = 10f;
    public int barkDamage = 10;

    float currentStamina;

    Rigidbody2D rb;
    Animator animator;
    [SerializeField] GameObject projectilePrefab;


    public Transform attackPoint;
    public LayerMask enemyLayers;

    Vector3 initialPosition;

    [SerializeField] Vector2 moveInput;

    bool isSprinting = false;
    public bool isAttacking = false;
    public bool canMoveDog = false;

  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentStamina = stamina;
        currentMoveSpeed = defaultMoveSpeed;
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        Walk();
        FlipSprite();
    }

    void Update()
    { 
        StaminaManagement();
        Die();
    }

    public void DisableInput()
    {
        moveInput = Vector2.zero;
    }

    void StaminaManagement()
    {
        // Stamina reduces when dog is sprinting
        if (isSprinting)
        {
            currentStamina -= staminaDepletionRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, stamina);

            // Dog stops spriting when stamina runs out
            if (currentStamina <= 0)
            {
                StopSprinting();
            }
        }
        else
        {
            // Regain staminia when not sprinting
            currentStamina += (stamina / 2) * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, stamina);
        }
    }

    void OnMove(InputValue value)
    {
        // Player can't move if dead
        if (!GameSession.Instance.isAlive && !canMoveDog)
        {
            return;
        }
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        // Gets value of player movement and true if greater than 0 
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        // Flips sprite by scaling the the x axis when player has horizonal speed
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }

        // Get input values from player 
        moveInput = value.Get<Vector2>();
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
        // Checks if the sprint button is held down and if the dog has enough stamina
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
        Debug.Log("Start sprint: " + currentStamina);
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
        isAttacking = true;
        // Detect enemies in rangef
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            
            // Perform a null check before trying to access the WeakPoint component
            WeakPoint weakPoint = enemy.GetComponent<WeakPoint>();
            if (weakPoint != null)
            {
                weakPoint.TakeDamage(barkDamage);
            }
            else
            {
                Debug.LogWarning("No WeakPoint component found on " + enemy.name);
            }
        }

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
        if (collision.gameObject.CompareTag("Ink"))
        {
            Ascend();
        }
    }

    void Ascend()
    {
        rb.velocity = new Vector2(rb.velocity.x, amplitude);
    }

    void Die()
    {
        if (!GameSession.Instance.isAlive)
        {
            animator.SetTrigger("isDead");
        }
    }
}