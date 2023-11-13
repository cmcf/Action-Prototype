using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    public GameObject VFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ignore"))
        {
            return;
        }
        PlayVFX();
        Destroy(gameObject);
    }

    void PlayVFX()
    {
        Vector3 vfxPosition = transform.position;
        GameObject vfxInstance = Instantiate(VFX, vfxPosition, transform.rotation);
    }
}
