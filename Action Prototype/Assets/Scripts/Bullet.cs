using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int damageAmount = 20;
    Enemy enemy;
    Crate crate;
    [SerializeField] GameObject barrel;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ignore")
        {
            return;
        }
        // When the bullet hits an enemy, the enemy and bullet is destroyed 
        if (other.tag == "Enemy")
        {
            enemy = FindObjectOfType<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            } 
        }
        if (other.tag == "Barrel")
        {
            crate = FindObjectOfType<Crate>();
            crate.DestroyCrate();
        } 
        Destroy(gameObject);
    }
}

