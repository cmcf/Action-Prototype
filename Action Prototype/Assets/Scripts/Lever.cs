using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    Animator animator;
    public bool isActivated = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isActivated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isActivated)
        {
            return;
        }
        // Lever is activated if player is within range and has interacted with the lever
        if (other.CompareTag("Player") && isActivated)
        {
            // Perform the lever activation action here (e.g., open a door).
            Debug.Log("Lever activated by player.");
            animator.SetBool("isActivated", true);
            Invoke("LeverDelay", 2f);
        }
    }

    void LeverDelay()
    {
        isActivated = false;
        animator.SetBool("isActivated", false);
    }
}
