using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    Enemy enemy;
    RedObject redObject;
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
        if (other.CompareTag("Red"))
        {
            redObject = other.GetComponent<RedObject>();
            redObject.DestroyObject();
        }

        Destroy(gameObject);
    }

}
