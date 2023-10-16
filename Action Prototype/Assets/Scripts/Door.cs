using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float timer = 5f;
    public bool isOpen;
    Animator animator;
    Lever lever;
    BoxCollider2D boxCollider;

    [System.Obsolete]
    private void Start()
    {
        animator = GetComponent<Animator>();
        lever = FindObjectOfType<Lever>();
        boxCollider= GetComponent<BoxCollider2D>();
    }
    public void OpenDoor()
    {
        Debug.Log("open");
        animator.SetBool("isOpen", true);
        
        Invoke("CloseDoor", timer);
    }
    public void CloseDoor()
    {
        animator.SetBool("isOpen", false);
        
        lever.LeverReset();
    }
}
