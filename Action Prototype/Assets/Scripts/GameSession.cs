using Abertay.Analytics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }
 
    public ScenePersist scenePersist;
    public GameObject checkpoint;
    public CharacterSwitcher characterSwitcher;

    public int lives = 3;
    public int keysCollected = 0;
    public bool isAlive = true;
 

    float delay = 0.001f;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayerHit()
    {
        TakeLife();
    }

    void TakeLife()
    {
        lives--;

        isAlive = false;
        StartCoroutine(Death());
    }


    private CharacterState currentCharacterState = CharacterState.Player;

    public CharacterState GetCurrentCharacterState()
    {
        return currentCharacterState;
    }

    IEnumerator Death()
    {
        // Stores player death location
        characterSwitcher.LogDeath();

        // Wait for a short delay before resetting alive state
        yield return new WaitForSeconds(delay);

        // Reset isAlive
        isAlive = true;
    }

    public void Quit()
    {
        // Save the name of the active scene
        PlayerPrefs.SetString("LastActiveScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Retrieve the saved scene name
        string lastActiveScene = PlayerPrefs.GetString("LastActiveScene");
        Debug.Log("Last Active Scene: " + lastActiveScene);
     
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("LastLevelPlayed", lastActiveScene);
        AnalyticsManager.SendCustomEvent("LastLevelName", data);

        Debug.Log("Quit");
        // Quits the game
        Application.Quit();
    }
}
