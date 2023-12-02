using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueObject : MonoBehaviour
{
    public string[] associatedRoomTags = { "HiddenRoomA", "HiddenRoomB" };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlueBullet"))
        {
            Debug.Log("Object Triggered");

            Destroy(gameObject);

            // Find all HiddenRoom scripts in the scene
            HiddenRoom[] hiddenRooms = FindObjectsOfType<HiddenRoom>();

            // Loop through all HiddenRoom scripts
            foreach (HiddenRoom hiddenRoom in hiddenRooms)
            {
                // Check if the HiddenRoom's tag is in the associatedRoomTags array
                if (Array.Exists(associatedRoomTags, tag => tag == hiddenRoom.tag))
                {
                    // Room is unhidden
                    hiddenRoom.RevealRoom();
                    Debug.Log("RevealRoom called for " + hiddenRoom.gameObject.name);
                }
            }
        }
    }
}

