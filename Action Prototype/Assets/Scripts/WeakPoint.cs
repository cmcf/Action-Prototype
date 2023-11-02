using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth = 2f;
    SpriteRenderer sprite;
    private void Start()
    {
        currentHealth = maxHealth;
        sprite = GetComponent<SpriteRenderer>();    
    }
    public void TakeDamage()
    {
        // Health is decreased when hit 
        currentHealth--;
        sprite.color = Color.red;
        Invoke("ResetColour", 0.2f);
        // Shield is destroyed when health reaches 0
        if (currentHealth <= 0)
        {
            Destroy(gameObject); 
        }
    }
    public Color ConvertHexToColor(int hex)
    {
        // Convert the hex integer to separate color components
        float r = ((hex >> 16) & 255) / 255f;
        float g = ((hex >> 8) & 255) / 255f;
        float b = (hex & 255) / 255f;

        return new Color(r, g, b, 1f); // Assumes full alpha
    }
    private void ResetColour()
    {
        Color spriteColor = ConvertHexToColor(0x07C4D6);
        sprite.color = spriteColor;
    }
}
