using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialPromps : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    BoxCollider2D firstCollider;
    [SerializeField] Image rmb;
    [SerializeField] Image lmb;
    
    void Start()
    {
        
        firstCollider = GetComponent<BoxCollider2D>();
        text.gameObject.SetActive(false);
        if (rmb != null)
        {
            rmb.gameObject.SetActive(false);
        }
        if (lmb != null)
        {
            lmb.gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Dog") || other.CompareTag("Bear"))
        {
            // Display the tutorial message
            ShowTutorialMessage();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Dog") || other.CompareTag("Bear"))
        {
            // Display the tutorial message
            HideMessage();
        }
    }

    private void ShowTutorialMessage()
    {
        text.gameObject.SetActive(true);
        if (rmb != null)
        {
            rmb.gameObject.SetActive(true);
        }
        if (lmb != null)
        {
            lmb.gameObject.SetActive(true);
        }
    }

    void HideMessage()
    {
        text.gameObject.SetActive(false);
        if (rmb != null)
        {
            rmb.gameObject.SetActive(false);
        }
        if (lmb != null)
        {
            lmb.gameObject.SetActive(false);
        }
    }
}
