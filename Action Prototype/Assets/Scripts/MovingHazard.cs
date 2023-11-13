using UnityEngine;


public class MovingHazard : MonoBehaviour
{
    [SerializeField] private float originalMoveSpeed = 5f;
    [SerializeField] private float minimumMoveSpeed = 2f;
    [SerializeField] private float pauseDelay = 1.0f;
    [SerializeField] private float decreaseSpeedAmount = 1.5f;

    public Transform movingObject;
    public Transform startPosition;
    public Transform endPosition;

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
            Debug.Log("Hit");
            SlowDown();
        }
    }

    private void SlowDown()
    {
        // Decrease the speed of the moving hazard
        originalMoveSpeed -= decreaseSpeedAmount;

        // Clamp the move speed so it doesn't go below a certain amount
        originalMoveSpeed = Mathf.Max(originalMoveSpeed, minimumMoveSpeed);
    }

}
