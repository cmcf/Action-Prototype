using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D rb;
    Animator anim;
    
    public int currentHealth;
    [SerializeField] int maxHealth = 80;
    WeakPoint weakPoint;
    [SerializeField] GameObject keyPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();

        currentHealth = maxHealth;
        // Use Transform.Find to locate the child GameObject with the WeakPoint component
        Transform childTransform = transform.Find("Weak Point");
        
        if (childTransform != null)
        {
            // Use GetComponent on the child GameObject to get the WeakPoint component
            weakPoint = childTransform.GetComponent<WeakPoint>();
        }
    }

    void Update()
    {
       
    }

    public void TakeDamage(int damage)
    {

        if (weakPoint.isDestroyed)
        {
            currentHealth -= damage;
            anim.SetBool("IsHit", true);
            Invoke("SetIsHitToFalse", 0.5f);
        }
        

        // Check if the enemy should be destroyed
        if (currentHealth <= 0)
        {
            Die();
        }   
    }
    void SetIsHitToFalse()
    {
        anim.SetBool("IsHit", false);
    }

    void Die()
    {
        Debug.Log("Enemy dead");
        Instantiate(keyPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
  
}
