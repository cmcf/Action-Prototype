using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public bool isDestroyed = false;
    [SerializeField] int maxHealth = 20;
    [SerializeField] int currentHealth;

    SpriteRenderer spriteRenderer;
    CircleCollider2D boxCollider;
    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<CircleCollider2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the enemy should be destroyed
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Weakpoint destroyed");
        isDestroyed = true;
        spriteRenderer.color = Color.green;
        boxCollider.enabled = false;
    }
}
