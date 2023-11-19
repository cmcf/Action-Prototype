using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f; // Player default movement speed
    [SerializeField] float jumpForce = 5f; // How high the player can jump 
    [SerializeField] int jumpsRemaining = 0; // Stores the amount of jumps the player has

    [SerializeField] Vector2 moveInput;

    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D playerCollider;
    BoxCollider2D feetCollider;

    public bool isGrounded = false;
    public bool canMovePlayer = true;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        
    }

    void FixedUpdate()
    {
        Run();
        FlipSprite();
    }
    void Update()
    { 
        GroundCheck();
        Die();
    }
    public void DisableInput()
    {
        moveInput = Vector2.zero;
    }

    void OnQuit(InputValue value)
    {
        Application.Quit();
    }

    void OnMove(InputValue value)
    {
        // Player can't move if dead
        if (!canMovePlayer || rb == null)
        {
            return;
        }
      
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
        int layerMask = LayerMask.GetMask("Ground", "Enemy");
        isGrounded = feetCollider.IsTouchingLayers(layerMask);

        // Reset jumps if player was in the air and landed on the ground
        if (!wasGrounded && isGrounded)
        {
            // Reset the number of jumps
            jumpsRemaining = 0;
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
    void Die()
    {
        if (!GameSession.Instance.isAlive) 
        {
            animator.SetTrigger("isDead");
        }   
    }
}

