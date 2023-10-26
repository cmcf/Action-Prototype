using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If key is collected by the player, increase amount of keys collected
        if (collision.CompareTag("Player") || collision.CompareTag("Dog") || collision.CompareTag("Bear"))
        {
            Destroy(gameObject);
            FindObjectOfType<GameSession>().keysCollected++;
        }
    }
}
