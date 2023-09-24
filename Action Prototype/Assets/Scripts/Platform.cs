using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float lifetime = 5f;
   
    void Start()
    {
        // Schedule the destruction of the platform
        Destroy(gameObject, lifetime);
    }
}
