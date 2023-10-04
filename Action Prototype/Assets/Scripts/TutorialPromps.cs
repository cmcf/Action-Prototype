using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialPromps : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    BoxCollider2D firstCollider;
    Canvas canvas;
    
    
    void Start()
    {
        
        firstCollider = GetComponent<BoxCollider2D>();
        text.gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Dog") || other.CompareTag("Bear"))  // Check if the entering object has the "Player" tag
        {
            // Display the tutorial message or perform any desired actions
            ShowTutorialMessage();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Dog") || other.CompareTag("Bear")) // Check if the entering object has the "Player" tag
        {
            // Display the tutorial message or perform any desired actions
            HideMessage();
        }
    }

    private void ShowTutorialMessage()
    {
        text.gameObject.SetActive(true);
    }

    void HideMessage()
    {
        text.gameObject.SetActive(false);
    }
}
