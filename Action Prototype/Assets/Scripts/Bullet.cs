using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BlueBullet : MonoBehaviour
{
    Shield shield;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield"))
        {
            Debug.Log("Blue bullet hit the shield.");
            shield = other.GetComponent<Shield>();
            shield.TakeDamage();
        }
        Destroy(gameObject);
    }
}

