using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        Debug.Log("open");
        animator.SetBool("isOpen", true);
    }
    public void CloseDoor()
    {
        animator.SetBool("isOpen", false);
    }
}
