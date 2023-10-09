using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speedDecreaseAmount = 0.25f;

    PlayerMovement playerMovement;
    Lever lever;
   
   

    public bool isAlive = true;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            playerMovement.DecreaseMovementSpeed(speedDecreaseAmount);
        }
    }

}

