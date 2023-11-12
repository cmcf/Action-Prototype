using System.Collections;
using UnityEngine;


public class MovingHazard : MonoBehaviour
{
    [SerializeField] float amplitude;
    [SerializeField] float frequency; // How fast the object moves
    [SerializeField] float decreaseAmount = 1.5f;
    [SerializeField] float minimumFrequencyValue = 4f;
    Vector3 initialPosition;
    private void Start()
    {
        initialPosition = transform.position;
    }
    private void Update()
    {
        // Moves object smootly over time on the Y axis
        transform.position = new Vector3(initialPosition.x, Mathf.Sin(Time.time * frequency) * amplitude + initialPosition.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RedBullet"))
        {
            StartCoroutine(SlowDown());
        }
    }
    private IEnumerator SlowDown()
    {
        // Decrease the speed of the moving hazard
        frequency -= decreaseAmount;

        // Clamp the frequency so it doesn't go below a certain amount
        frequency = Mathf.Max(frequency, minimumFrequencyValue);
    }
}