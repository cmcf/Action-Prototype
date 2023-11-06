using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    Enemy enemy;
    [System.Obsolete]

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ignore"))
        {
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Red bullet hit the enemy.");
            enemy = other.GetComponent<Enemy>();
            enemy.EnemyDeath();
        }

        Destroy(gameObject);
    }

}
