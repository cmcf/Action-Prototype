using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject inkPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] float fireDelay = 0.1f;
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] float inkSpeed = 1.0f;
    [SerializeField] float inkForwardSpeed = 1f;
    [SerializeField] float inkCheckRadius = 0.2f;

    public Image inkImage;
    public Image bulletImage;

    Animator animator;
    PlayerMovement playerMovement;

    bool canFire = true;

    void Start()
    {
        
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        UpdateInkUI();
    }

    void UpdateInkUI()
    {
        // Checks if player can fire ink in current area
        bool inkAlreadyPresent = CheckForInk();

        if (!inkAlreadyPresent)
        {
            inkImage.color = Color.white;
        }
        else
        {
            inkImage.color = Color.grey;
        }
        if (canFire)
        {
            bulletImage.color = Color.red;
        }
        else
        {
            bulletImage.color = Color.black;
        }
    }

    void OnFire(InputValue value)
    { 
        if(canFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        // CanFire is set to false stop the player firing without a delay
        canFire = false;
        // Enables animation
        animator.SetBool("isFiring", true);
        // Instantiate a new bullet from the bullet prefab
        GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);


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
            // Calculates the bullet's velocity based on the player's direction
            Vector2 bulletVelocity = new Vector2(bulletSpeed * direction, 0f);

            // Set the bullet's velocity
            bulletRigidbody.velocity = bulletVelocity;
            // Fire delay is called which sets can fire back to true after a delay 
            Invoke("FireDelay", fireDelay);
        }
    }

    void OnInk(InputValue value)
    {
        // Check if there's already ink at the desired spawn point
        bool inkAlreadyPresent = CheckForInk();

        if (!inkAlreadyPresent)
        {
            // Set the ink UI colour to white to indicate that the player can fire
            inkImage.color = Color.white;

            StartCoroutine(FireInk());
        }
        else
        {
            // Set the ink UI colour to grey to indicate that the player can't fire
            inkImage.color = Color.grey;
        }
    }
    bool CheckForInk()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(spawnPoint.position, inkCheckRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Ink"))
            {
                return true;
            }
        }

        return false;
    }
    IEnumerator FireInk()
    {
        yield return new WaitForSeconds(0.1f);

        // Reset ink UI after firing
        inkImage.color = Color.grey;

        if (playerMovement.isGrounded)
        {
            // Enables animation
            animator.SetBool("isFiring", true);
            // Create a rotation
            Quaternion inkRotation = Quaternion.Euler(0f, 0f, 90f); // Rotate by 45 degrees around the Z-axis
                                                                    // Spawns ink
            GameObject newInk = Instantiate(inkPrefab, spawnPoint.position, spawnPoint.rotation);
            // Add a force to make the ink move downwards
            Rigidbody2D inkRigidbody = newInk.GetComponent<Rigidbody2D>();
            if (inkRigidbody != null)
            {
                // Adjust the force values as needed to control the speed and direction
                Vector2 downwardForce = Vector2.down * inkSpeed;
                Vector2 forwardForce = Vector2.right * inkForwardSpeed; // Adjust forwardSpeed as needed
                inkRigidbody.AddForce(downwardForce + forwardForce, ForceMode2D.Impulse);
            }
            // Fire delay is called which sets can fire back to true after a delay 
            Invoke("FireDelay", fireDelay);
        }
    }

void FireDelay()
    {
        // Enables the player to fire again and stops firing animation
        canFire = true;
        inkImage.color = Color.white;
        animator.SetBool("isFiring", false);
    }

}
