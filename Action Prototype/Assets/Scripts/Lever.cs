using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    Animator animator;

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

    }
}
