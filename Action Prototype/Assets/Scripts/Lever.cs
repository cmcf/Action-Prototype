using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    Animator animator;
    Door door;

    [System.Obsolete]
    private void Start()
    {
        animator = GetComponent<Animator>();
        door = FindObjectOfType<Door>();
    }

    public void LeverPressed()
    {
        // Animation is played when the player interacts with the lever and door opens
        animator.SetBool("isActivated", true);  
        door.OpenDoor();
    }
    public void LeverReset()
    {
        animator.SetBool("isActivated", false);
    }
}
