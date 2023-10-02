using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    Animator animator;
    public bool isActivated;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
     
    }

    public void LeverPressed()
    {
        // Perform the lever activation action here (e.g., open a door).
        Debug.Log("Lever activated by player.");
        animator.SetBool("isActivated", true);
        Invoke("LeverDelay", 10f);

    }

    void LeverDelay()
    {
        isActivated = false;
        animator.SetBool("isActivated", false);
    }
}
