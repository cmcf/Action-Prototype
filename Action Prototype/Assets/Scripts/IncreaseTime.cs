using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseTime : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"|| collision.tag == "Dog")
        {
            Timer.Instance.IncreaseTime();
            Destroy(gameObject);
        }
    }
}
