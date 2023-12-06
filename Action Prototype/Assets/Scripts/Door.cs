using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float timer = 5f;
    public bool isOpen;
    Animator animator;
    Lever lever;
    private AudioSource doorAudioSource;
    public AudioClip timeSFX;

    [System.Obsolete]
    private void Start()
    {
        animator = GetComponent<Animator>();
        lever = FindObjectOfType<Lever>();
        doorAudioSource = GetComponent<AudioSource>();
    }
    public void OpenDoor()
    {
        Debug.Log("open");
        animator.SetBool("isOpen", true);

        // Check if doorAudioSource is not null and if it is not currently playing
        if (doorAudioSource != null && !doorAudioSource.isPlaying)
        {
            doorAudioSource.Play();
        }
        Invoke("CloseDoor", timer);
    }
    public void CloseDoor()
    {
        if (doorAudioSource != null && doorAudioSource.isPlaying)
        {
            doorAudioSource.Stop();
        }
        animator.SetBool("isOpen", false);
        lever.LeverReset();
    }
}
