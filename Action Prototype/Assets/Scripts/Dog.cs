using UnityEngine;
using UnityEngine.InputSystem;

public class Dog : MonoBehaviour
{
    [SerializeField] float defaultMoveSpeed = 5f;
    [SerializeField] float currentMoveSpeed; // Default movement speed
    [SerializeField] float sprintSpeed = 10f; // Speed when sprinting
    [SerializeField]float stamina = 80f;      // Maximum stamina
    [SerializeField] float staminaDepletionRate = 10f;

    float currentStamina;

    Rigidbody2D rb;
    Animator animator;

    Vector2 moveInput;

    bool isAlive = true;
    bool isSprinting = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentStamina = stamina;
        currentMoveSpeed = defaultMoveSpeed;
    }

    void Update()
    {
        FlipSprite();
        Walk();
        StaminaManagement();
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
}