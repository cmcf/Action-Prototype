using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Security.Cryptography;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    public ScenePersist scenePersist;

    public int lives = 3;
    public int keysCollected = 0;
    public bool isAlive = true;

    float loadLevelDelay = 0.8f;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayerHit()
    {
        TakeLife();
    }

    // Reloads current level
    void TakeLife()
    {
        lives--;
        isAlive = false;
        Invoke("ReloadScene", loadLevelDelay);
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isAlive = true;
    }
}
