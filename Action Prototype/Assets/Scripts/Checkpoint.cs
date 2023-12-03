using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Reference to the CharacterSwitcher script
    public CharacterSwitcher characterSwitcher;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Dog"))
        {
            Debug.Log("Checkpoint reached");

            // Increments the spawn index in the CharacterSwitcher script
            characterSwitcher.UpdateSpawnIndex((characterSwitcher.currentSpawnPointIndex + 1) % characterSwitcher.playerSpawnPoints.Length);

            gameObject.SetActive(false);
        }
    }
}
