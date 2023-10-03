using UnityEngine;


public class MovingHazard : MonoBehaviour
{
    [SerializeField] float amplitude;
    [SerializeField] float frequency; // How fast the object moves
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
    }
}