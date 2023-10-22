using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroInteract : MonoBehaviour
{
    public bool inRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;



    void Update()
    {
        // Checks if player is in range of the interactible and the player has pressed the key
        if (inRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                // Call event
                interactAction.Invoke();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
}
