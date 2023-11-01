using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Security.Cryptography;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }
    Timer timer;

    public ScenePersist scenePersist;

    public int lives = 3;
    public int keysCollected = 0;
    public bool isAlive = true;

    private void Awake()
    {
        Instance = this;
    }

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
        Invoke("ReloadScene", 0.8f);
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isAlive = true;
    }
}
