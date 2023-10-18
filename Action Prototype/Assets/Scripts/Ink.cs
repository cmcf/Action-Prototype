using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with something");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Invoke("DestroyInk", 5f);
    }

    void DestroyInk()
    {
        Destroy(gameObject);
    }
}
