using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    [System.Obsolete]

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ignore"))
        {
            return;
        }

        Destroy(gameObject);
    }

}
