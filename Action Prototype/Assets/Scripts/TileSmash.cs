using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSmash : MonoBehaviour
{
    public void BreakTile()
    {
        Invoke("DestroyTile", 0.5f);
    }

    void DestroyTile()
    {
        Destroy(gameObject);
    }
}


