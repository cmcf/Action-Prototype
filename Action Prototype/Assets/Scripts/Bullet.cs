using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int damageAmount = 20;
    Enemy enemy;
    void OnTriggerEnter2D(Collider2D other)
    {
        // When the bullet hits an enemy, the enemy and bullet is destroyed 
        if (other.tag == "Enemy")
        {
            enemy = FindObjectOfType<Enemy>();
            enemy.TakeDamage(damageAmount);
        }
        Destroy(gameObject);
    }
}

