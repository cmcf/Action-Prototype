using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Dog") || collision.CompareTag("Bear"))
        {
            GameSession.Instance.PlayerHit();
        }
    }
}
