using UnityEngine;
using Abertay.Analytics;
using System.Collections.Generic;

/* The moving hazard script was adapted based on insights from the tutorial "Unity Tutorial - How to Make Moving Platform in Unity | Unity Tutorial for Beginners" by Pix and Dev. 
 * The adjustments focused on improving the fluidity of hazard movement. 
 * The tutorial can be found on YouTube at https://www.youtube.com/watch?v=vua2a_Z3zlY
 */

public class MovingHazard : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float minimumMoveSpeed = 2f;
    [SerializeField] private float pauseDelay = 1.0f;
    [SerializeField] private float decreaseSpeedAmount = 1.5f;

    public Transform movingObject;
    public Transform startPosition;
    public Transform endPosition;
    public AudioClip hitSFX;

    public bool canRotate = false;

    int direction = 1;

    private void Update()
    {
        // Calculate the target position where the moving object is moving towards
        Vector2 target = currentTargetPosition();
        // Move the object towards the target using Vector2.Lerp for smooth movement
        movingObject.position = Vector2.Lerp(movingObject.position, target, moveSpeed * Time.deltaTime);

        float distance = (target - (Vector2)movingObject.position).magnitude;

        // Checks if the object is  close to the target
        if (distance <= 0.1f)
        {
            direction *= -1;
        }

        if (canRotate)
        {
            float degreesPerSecond = 40;
            
            transform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);
           
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
                SlowDown();
                LogSlowDownEvent();
            }
        }
    }

    private void SlowDown()
    {
        // Decrease the speed of the moving hazard
        moveSpeed -= decreaseSpeedAmount;

        // Clamp the move speed so it doesn't go below a certain amount
        moveSpeed = Mathf.Max(moveSpeed, minimumMoveSpeed);
        if (moveSpeed > minimumMoveSpeed)
        {
            // Play sound
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, 0.5f);
        }
       
    }
    private void LogSlowDownEvent()
    {
        // Collects the movement speed of the object each time it has been slowed down and stores the object name
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("ObjectSpeed", moveSpeed);
        data.Add("ObjectID", gameObject.name);

        AnalyticsManager.SendCustomEvent("SlowedDownObject", data);
    }
}
