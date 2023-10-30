using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSizeDecrease : MonoBehaviour
{
    [SerializeField] float decreaseDelay = 0.1f; // The delay before the platform reduces in size
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dog"))
        {
            StartCoroutine(DecreasePlatform());
        }
        if (collision.gameObject.CompareTag("Player")) 
        {
            anim.SetTrigger("SizeDown");
            DestroyPlatform();
        }
    }

    private IEnumerator DecreasePlatform()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(decreaseDelay);

        anim.SetTrigger("SizeDown");
        Invoke("DestroyPlatform", 0.4f);

    }

    void DestroyPlatform()
    {
        gameObject.SetActive(false);
    }
}
