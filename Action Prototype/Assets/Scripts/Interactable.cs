using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool inRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    
    Door door;

    [System.Obsolete]
    void Start()
    {
        door = FindObjectOfType<Door>();
    }

    
    void Update()
    {
        // Checks if player is in range of the interactible and the player has pressed the key
        if (inRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                // Call event
                interactAction.Invoke();
                
                door.OpenDoor();
                Invoke("Timer", 10f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("In range");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        Debug.Log("No range");
    }

    private void Timer()
    {
        door.CloseDoor();
    }
}
