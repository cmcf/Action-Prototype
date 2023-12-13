using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    float defaultMoveSpeed = 3.5f; // Default movement speed
    float maxStamina = 20; // Max dog stamina
    [SerializeField] float moveSpeed = 3.2f; // Player current movement speed
    [SerializeField] float jumpMoveSpeed = 2.5f; // Player movement speed in air
    [SerializeField] float jumpForce = 5f; // How high the player can jump 
    [SerializeField] int jumpsRemaining = 0; // Stores the amount of jumps the player has

    [SerializeField] Vector2 moveInput;

    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D playerCollider;
    BoxCollider2D feetCollider;

    public Dog dog;
    public AudioClip jumpSFX;
    public bool isGrounded = false;
    public bool canMovePlayer = true;


    void Start()
    {
        // References
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();

    }

    void FixedUpdate()
    {
        Run();
        FlipSprite();
        GroundCheck();
    }
    void Update()
    {
        Die();
    }
    public void DisableInput()
    {
        moveInput = Vector2.zero;
    }

    void OnQuit(InputValue value)
    {
        GameSession.Instance.Quit();
    }

    void OnMove(InputValue value)
    {


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

    void Run()
    {
        // X velocity is multiplied by the move speed and retains current velocity on the y axis
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        // Checks if player is moving left or right
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        // Sets animation state to run
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }
    void OnJump(InputValue value)
    {
        // Player can't jump if they are not alive
        if (!GameSession.Instance.isAlive)
        {
            return;
        }

        // Checks if the player is grounded or has remaining jumps
        if (!isGrounded && jumpsRemaining <= 0)
        {
            return;
        }
        // Jump is called when space button is pressed
        if (value.isPressed)
        {
            Jump();
        }
    }

    void Jump()
    {
        moveSpeed = jumpMoveSpeed;
        AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position, 2f);
        // Check if the player is on a moving platform and unparent
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        if (isGrounded || jumpsRemaining > 0)
        {
            // play jump SFX at camera location.
            // AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position, 0.8f);

            // player moves up by jump force amount.
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Decreases the number of remaining jumps only if not grounded
            if (!isGrounded)
            {
                jumpsRemaining--;
            }
        }
    }
    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        int layerMask = LayerMask.GetMask("Ground", "Enemy", "Hidden");
        isGrounded = feetCollider.IsTouchingLayers(layerMask);

        // Reset jumps if player was in the air and landed on the ground
        if (!wasGrounded && isGrounded)
        {
            // Reset the number of jumps and move speed
            jumpsRemaining = 0;
            moveSpeed = defaultMoveSpeed;
        }
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
        if (collision.gameObject.CompareTag("Hidden"))
        {
            // Disable the player's collider
            DisablePlayerCollision(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hidden"))
        {
            // Enable the player's collider when exiting the hidden tiles
            DisablePlayerCollision(false);
        }
    }

    private void DisablePlayerCollision(bool disable)
    {
        // Assuming the player has a Collider2D component
        Collider2D playerCollider = GetComponent<Collider2D>();

        if (playerCollider != null)
        {
            playerCollider.enabled = !disable;
        }

        
    }
    void Die()
    {
        if (!GameSession.Instance.isAlive)
        {
            animator.SetBool("isDead", true);
            // Resets dog stamina
            dog.currentStamina = maxStamina; 
        }
        else
        {
            animator.SetBool("isDead", false);
        }
    }
}

