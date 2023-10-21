using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Security.Cryptography;

public class GameSession : MonoBehaviour
{
    Timer timer;

    public ScenePersist scenePersist;

    public int lives = 3;
    public int keysCollected = 0;
    public bool isAlive = true;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
    }
    public void PlayerHit()
    {
        TakeLife();
    }

    // Decreases player lives by 1 and reloads current level
    void TakeLife()
    {
        lives--;
        isAlive = false;   
        ReloadScene();
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isAlive = true;
    }
}
