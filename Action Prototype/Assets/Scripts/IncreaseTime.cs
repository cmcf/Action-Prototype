using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseTime : MonoBehaviour
{
    Timer timer;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"|| collision.tag == "Dog")
        {
            Debug.Log("Picked up");
            timer.IncreaseTime();
            Destroy(gameObject);
        }
    }
}
