using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float lifetime = 5f;
    public event Action<GameObject> OnPlatformDestroyed; // Event to notify listeners
    // Start is called before the first frame update
    void Start()
    {
        // Schedule the destruction of the GameObject
        Destroy(gameObject, lifetime);
    }
}
