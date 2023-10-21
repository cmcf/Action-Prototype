using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    [System.Obsolete]
    void Awake()
    {
        // Find GameManager and attach this ScenePersist to it
        GameSession gameSession = FindObjectOfType<GameSession>();

        if (gameSession != null)
        {
            gameSession.scenePersist = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("GameManager not found. ScenePersist won't work as expected.");
        }
    }

    public void ResetScene()
    {
        Destroy(gameObject);
    }
}

