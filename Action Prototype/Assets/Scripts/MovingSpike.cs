using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpike : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public Vector3 moveDirection = Vector3.right;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new position of the tile
        Vector3 newPosition = initialPosition + moveDirection * Mathf.Sin(Time.time * moveSpeed);

        // Update the tile's position
        transform.position = newPosition;
    }
}
