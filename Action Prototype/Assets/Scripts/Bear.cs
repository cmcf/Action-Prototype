using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bear : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f; // Default movement speed
    [SerializeField] float attackDelay = 1f;

    Rigidbody2D rb;
    Animator animator;

    Vector2 moveInput;

    bool isAlive = true;
    bool canAttack = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FlipSprite();
        Walk();
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

        // Get input values from player 
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (canAttack)
        {
            // CanFire is set to false stop the player firing without a delay
            canAttack = false;
            Debug.Log("bear attack");
            animator.SetBool("isAttacking", true);
            // Fire delay is called which sets can fire back to true after a delay 
            Invoke("AttackDelay", attackDelay);

        }
    }

    void AttackDelay()
    {
        canAttack = true;
        animator.SetBool("isAttacking", false);
    }


    void Walk()
    {
        // X velocity is multiplied by the move speed and retains current velocity on the y axis
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        // Checks if player is moving left or right
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        // Sets animation state to run
        animator.SetBool("isWalking", playerHasHorizontalSpeed);
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
}

