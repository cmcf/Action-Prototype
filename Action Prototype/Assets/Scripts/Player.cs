using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject platform; // Platform reference
    public float minDistance = 2.0f; // Minimum distance from nearby objects
    public LayerMask obstacleLayer;  // Layer(s) to consider as obstacles
    public Transform playerTransform; // Reference to the player's Transform
    //bool canFire = true;

    private List<Vector3> placedPlatformPositions = new List<Vector3>(); // Keeps track of the positions of platforms that the player has already placed
    

    void Start()
    {

    }

    void Update()
    {
        SpawnPlatform();
    }

    void SpawnPlatform()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            // Convert mouse position to world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the platform is at the same Z position as your 2D objects

            // Check if the spawn position is valid
            if (IsSpawnPositionValid(mousePosition))
            {
                // Spawn the platform at the mouse position
                Instantiate(platform, mousePosition, Quaternion.identity);

                // Add the spawn position to the list of placed platforms
                placedPlatformPositions.Add(mousePosition);
            }
        }
    }

    bool IsSpawnPositionValid(Vector3 position)
    {
        // Check if the spawn position is too close to other platforms, the player, or previously placed platforms
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, minDistance, obstacleLayer);
        foreach (var collider in colliders)
        {
            if (collider.transform != playerTransform)
            {
                return false; // Too close to an obstacle
            }
        }

        // Check if the spawn position overlaps with any previously placed platforms
        foreach (var placedPosition in placedPlatformPositions)
        {
            if (Vector3.Distance(position, placedPosition) < minDistance)
            {
                return false; // Too close to a previously placed platform
            }
        }

        return true; // Valid spawn position
    }

}
