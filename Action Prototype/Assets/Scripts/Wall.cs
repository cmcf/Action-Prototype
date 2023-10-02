using UnityEngine;

public class Wall : MonoBehaviour
{
    Animator anim;
   
    private void Start()
    {
       anim = GetComponent<Animator>();  
    }

    public void BreakWall()
    {
        anim.SetTrigger("isDestroyed");
        Invoke("DestroyWall", 0.8f);
    }
    void DestroyWall()
    {
        Destroy(gameObject);
    }
}
