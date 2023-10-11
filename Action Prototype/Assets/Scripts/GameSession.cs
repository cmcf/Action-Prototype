using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Security.Cryptography;

public class GameSession : MonoBehaviour
{
    public int lives = 3;
    public int keysCollected = 0;
    public void PlayerHit()
    {
        TakeLife();
        Debug.Log("Lives reduced");
    }

    // Decreases player lives by 1 and reloads current level
    void TakeLife()
    {
        lives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
