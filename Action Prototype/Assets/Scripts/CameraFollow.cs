using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target to follow
    public float smoothSpeed = 0.125f; // The smoothness of the camera's movement
    public Vector3 offset = new Vector3(0f, 0f, 0f); // Adjust the offset as needed

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Debugging information
        Debug.Log("Camera Position: " + transform.position);
        Debug.Log("Target Position: " + target.position);
    }
}

