using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpForce = 5f; // How high the player can jump 
    [SerializeField] int maxJumps = 2; // Maximum amount of times the player can jump at once
    [SerializeField] int jumpsRemaining;
    Vector2 moveInput;

    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D playerCollider;
    BoxCollider2D feetCollider;

    bool isAlive = true;
    bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        // Player can't move if dead
        if (!isAlive)
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

        // Gets input values from player 
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
        //  Checks if the player is alive, and whether the jump button is pressed. If the player is grounded, the player does a regular jump.
        if (!isAlive)
        {
            return;
        }

        // checks if the player is grounded or has remaining jumps. If the player is in the air and has remaining jumps, the player double jumps 
        if (!isGrounded && jumpsRemaining <= 0)
        {
            return;
        }

        if (value.isPressed)
        {
            Jump();
        }
    }

    void Jump()
    {
        isGrounded = true;
        // play jump SFX at camera location
        //AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position, 0.8f);
        // player moves up by jump force amount
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpsRemaining--;
    }
    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        // Reset jumps if player was in the air and landed on the ground
        if (!wasGrounded && isGrounded)
        {
            jumpsRemaining = 2; // Reset the number of jumps
        }
    }

    void FlipSprite()
    {
        // Gets value of player movement and true if greater than 0 
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        // Flips sprite by scaling the the x axis when player has horizonal speed
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }

    }
}
