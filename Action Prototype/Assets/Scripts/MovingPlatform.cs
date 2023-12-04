using Abertay.Analytics;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float minimumMoveSpeed = 2f;
    [SerializeField] private float pauseDelay = 1.0f;
    [SerializeField] private float decreaseSpeedAmount = 1.5f;

    public Transform movingObject;
    public Transform startPosition;
    public Transform endPosition;
    Rigidbody2D rb;

    private Coroutine unparentCoroutine;
    int direction = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    private void FixedUpdate()
    {
        Vector2 target = currentTargetPosition();
        movingObject.position = Vector2.Lerp(movingObject.position, target, moveSpeed * Time.deltaTime);

        float distance = (target - (Vector2)movingObject.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    Vector2 currentTargetPosition()
    {
        if (direction == 1)
        {
            return startPosition.position;
        }
        else
        {
            return endPosition.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("RedBullet"))
        {
            SlowDown();
            LogSlowDownEvent();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y)
        {
            // Cancel any existing coroutine
            if (unparentCoroutine != null)
            {
                StopCoroutine(unparentCoroutine);
            }

            // Set the parent immediately
            collision.transform.SetParent(transform);

            // Start a coroutine to unparent after a short delay
            unparentCoroutine = StartCoroutine(DelayedUnparent(collision.transform));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Cancel any existing coroutine
        if (unparentCoroutine != null)
        {
            StopCoroutine(unparentCoroutine);
        }

        // Set the parent immediately
        collision.transform.SetParent(null);
    }

    private IEnumerator DelayedUnparent(Transform child)
    {
        // Wait for a short delay before unparenting
        yield return new WaitForSeconds(0.1f);

        // Unparent the child after the delay
        child.SetParent(null);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void SlowDown()
    {
        // Decrease the speed of the moving hazard
        moveSpeed -= decreaseSpeedAmount;

        // Clamp the move speed so it doesn't go below a certain amount
        moveSpeed = Mathf.Max(moveSpeed, minimumMoveSpeed);
    }
    private void LogSlowDownEvent()
    {

        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("ObjectSpeed", moveSpeed);
        data.Add("ObjectID", gameObject.name);

        // Convert the dictionary to a string for debugging
        string debugString = "LogSlowDownEvent - Data: ";
        foreach (var entry in data)
        {
            debugString += $"{entry.Key}: {entry.Value}, ";
        }

        // Remove the trailing comma and space
        debugString = debugString.TrimEnd(',', ' ');

        // Log the debug string
        Debug.Log(debugString);

        AnalyticsManager.SendCustomEvent("SlowedDownObject", data);
    }

}
