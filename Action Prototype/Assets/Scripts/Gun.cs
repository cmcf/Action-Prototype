using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] float fireDelay = 1f;
    [SerializeField] float bulletSpeed = 15f;

    Player player;

    bool canFire = true;

   
   
    void Start()
    {
        player = GetComponent<Player>();
    }

    void OnFire(InputValue value)
    { 
        if(canFire)
        {
            // CanFire is set to false stop the player firing without a delay
            canFire = false;
            // Instantiate a new bullet from the bullet prefab
            GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

            // Calculate the bullet's direction based on the player's scale
            float direction = player.transform.localScale.x > 0 ? 1f : -1f;

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
                float bulletSpeed = 8f; // Set your desired bullet speed
                Vector2 bulletVelocity = new Vector2(bulletSpeed * direction, 0f);

                // Set the bullet's velocity
                bulletRigidbody.velocity = bulletVelocity;
                // Fire delay is called which sets can fire back to true after a delay 
                Invoke("FireDelay", fireDelay);
            }
        }
    }  
    void FireDelay()
    {
        canFire = true;
    }

}
