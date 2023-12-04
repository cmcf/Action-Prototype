using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float pauseDelay = 2f;
    Rigidbody2D rb;
    private Vector2 initialPosition;
    private bool movingForward = true;
    public Vector2 platformVelocity;
 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartMoving();
    }

    
    void FixedUpdate()
    {
        platformVelocity = rb.velocity;
        // Checks if the platform is moving forward and if it has reached it's destination (+1 from position)  
        if (movingForward && transform.position.x >= initialPosition.x + 1f)
        {
            // Sets movement to zero which stops the platform from moving 
            rb.velocity = Vector2.zero;
            movingForward = false;
            // Start moving is called after the plarform has stopped for the duration of the delay 
            Invoke("StartMoving", pauseDelay);
        }
        // If the platform is moving backwards and has reached its destination, the platform stops 
        else if (!movingForward && transform.position.x <= initialPosition.x - 1f)
        {
            rb.velocity = Vector2.zero;
            movingForward = true;
            Invoke("StartMoving", pauseDelay);
        }
    }

    void StartMoving()
    {
        // Moves the platform forwards 
        if (movingForward)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            // Platform moves backwards if it is not set to move forwards
            rb.velocity = Vector2.left * speed;
        }
    }

    public Vector2 GetPlatformVelocity()
    {
        return platformVelocity;
    }
}
