using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float inkdesolveTime = 8f;
    [SerializeField] GameObject dogPrompt;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Invoke("DestroyInk", inkdesolveTime);

        if (collision.gameObject.CompareTag("Dog"))
        {
            dogPrompt.SetActive(false);
        }
    }

    void DestroyInk()
    {
        Destroy(gameObject);
    }
}
