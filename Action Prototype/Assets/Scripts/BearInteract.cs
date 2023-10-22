using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BearInteract : MonoBehaviour
{
    public bool inRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public bool canBreakWall;

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
        if (collision.gameObject.CompareTag("Dog"))
        {
            inRange = true;
            canBreakWall = true;
            Debug.Log("In range");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        canBreakWall = false;
        Debug.Log("No range");
    }
}
