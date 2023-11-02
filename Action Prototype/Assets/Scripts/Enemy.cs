using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject shieldObject;
    private Shield shield;
    private bool shieldDestroyed;

    private void Start()
    {
        shield = shieldObject.GetComponent<Shield>();
        shieldDestroyed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RedBullet") && shieldDestroyed)
        {
            Debug.Log("Red bullet hit the enemy.");
            // Red bullet destroys the enemy
            Destroy(gameObject);
        }
    }

    public void DestroyShield()
    {
        // Called when the shield is destroyed, enabling vulnerability to red bullets
        shieldDestroyed = true;
    }

}
