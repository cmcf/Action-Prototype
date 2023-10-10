using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Key keys = GetComponent<Key>();
        if (keys.keysCollected == 2)
        {
            Debug.Log("Open door");
        }
    }
}
