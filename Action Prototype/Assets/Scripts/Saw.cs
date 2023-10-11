using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    bool sawMoved = false;
   [SerializeField] float moveValue = 0.6f;
   public void MoveSaw()
    {
        if(sawMoved)
        {
            return;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + moveValue, transform.position.z);
        sawMoved = true;
    }
}
