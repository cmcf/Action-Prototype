using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float timer = 5f;
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
        Invoke("CloseDoor", 4f);
    }
    public void CloseDoor()
    {
        animator.SetBool("isOpen", false);
    }
}
