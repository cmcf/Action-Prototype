using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSizeDecrease : MonoBehaviour
{
    [SerializeField] float decreaseDelay = 0.1f; // The delay before the platform reduces in size
    Animator anim;
    Vector3 originalScale; // Stores the original scale of the platform

    private void Start()
    {
        anim = GetComponent<Animator>();
        originalScale = transform.localScale; // Store the original scale during initialization
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
            Invoke("ResetPlatform", decreaseDelay);
        }
    }

    private IEnumerator DecreasePlatform()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(decreaseDelay);

        anim.SetTrigger("SizeDown");
        Invoke("DestroyPlatform", 0.2f);
    }

    void ResetPlatform()
    {
        // Reset the platform to its original size
        transform.localScale = originalScale;
        Invoke("DestroyPlatform", 0.4f);
    }

    void DestroyPlatform()
    {
        // Removes platforms from the scene and calls the reappear function after a delay
        gameObject.SetActive(false);
        Invoke("PlatformAppear", 2f);
    }

    void PlatformAppear()
    {
        transform.localScale = originalScale;
        gameObject.SetActive(true);
    }
}
