using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject shieldObject;
    private Shield shield;
    public GameObject keyPrefab;


    public bool shieldDestroyed;

    private void Start()
    {
        shield = shieldObject.GetComponent<Shield>();
        shieldDestroyed = false;
    }

    public void DestroyShield()
    {
        // Called when the shield is destroyed, enabling vulnerability to red bullets
        shieldDestroyed = true;
    }

    public void EnemyDeath()
    {
        // Spawn offset 
        Vector3 spawnPosition = transform.position - new Vector3(0f, 0.5f, 0f);
        Instantiate(keyPrefab, spawnPosition, Quaternion.identity);
        Destroy(gameObject);
    }

}
