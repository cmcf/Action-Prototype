using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BlueBullet : MonoBehaviour
{
    Shield shield;
    BlueObject blueObject;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ignore"))
        {
            return;
        }
        if (other.CompareTag("Shield"))
        {
            shield = other.GetComponent<Shield>();
            shield.TakeDamage();
        }
        if (other.CompareTag("Blue"))
        {
            blueObject= other.GetComponent<BlueObject>();
            blueObject.DestroyObject();
        }
        Destroy(gameObject);
    }
}

