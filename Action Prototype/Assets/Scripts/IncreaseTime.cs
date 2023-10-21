using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseTime : MonoBehaviour
{
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"|| collision.tag == "Dog")
        {
            timer.IncreaseTime();
            Destroy(gameObject);
        }
    }
}
