using UnityEngine;
using Abertay.Analytics;
using System.Collections.Generic;

public class MovingHazard : MonoBehaviour
{
    [SerializeField] private float originalMoveSpeed = 5f;
    [SerializeField] private float minimumMoveSpeed = 2f;
    [SerializeField] private float pauseDelay = 1.0f;
    [SerializeField] private float decreaseSpeedAmount = 1.5f;

    public Transform movingObject;
    public Transform startPosition;
    public Transform endPosition;

    private float slowdownAmount;

    int direction = 1;

    private void Update()
    {
        Vector2 target = currentTargetPosition();
        movingObject.position = Vector2.Lerp(movingObject.position, target, originalMoveSpeed * Time.deltaTime);

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
            if (collision.CompareTag("RedBullet"))
            {
                slowdownAmount = SlowDown(); // Get the slowdown amount
                LogSlowDownEvent();
            }
        }
    }

    private float SlowDown()
    {
        // Calculate slowdown amount based on the initial speed
        slowdownAmount = originalMoveSpeed * decreaseSpeedAmount;

        // Decrease the speed of the moving hazard
        originalMoveSpeed -= slowdownAmount;

        // Clamp the move speed so it doesn't go below a certain amount
        originalMoveSpeed = Mathf.Max(originalMoveSpeed, minimumMoveSpeed);

        return slowdownAmount;
    }

    private void LogSlowDownEvent()
    {

        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("ObjectID", gameObject.name);
        data.Add("SlowdownAmount", slowdownAmount);


        // Convert the dictionary to a string for debugging
        string debugString = "LogSlowDownEvent - Data: ";
        foreach (var entry in data)
        {
            debugString += $"{entry.Key}: {entry.Value}, ";
        }
        Debug.Log(debugString);

        AnalyticsManager.SendCustomEvent("SlowedDownObject", data);
    }
}
