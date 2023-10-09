using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D rb;

    public int currentHealth;
    int maxHealth = 80;
    WeakPoint weakPoint;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        weakPoint = GetComponent<WeakPoint>();
    }

    void Update()
    {
       
    }

    public void TakeDamage(int damage)
    {
        if (weakPoint.isDestroyed)
        {
            currentHealth -= damage;
        }

         // Check if the enemy should be destroyed
        if (currentHealth <= 0)
        {
            Die();
        }   
    }

    void Die()
    {
        Debug.Log("Enemy dead");
    }
  
}
