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
    int timesPlayerHasDied = 0;

    float delay = 0.66f;

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
        timesPlayerHasDied++;

        Color c = Color.red;
        c.a = 0.4f;
        AnalyticsManager.LogHeatmapEvent("TotalDeaths", transform.position, c);

        isAlive = false;
        StartCoroutine(DeathAnimationReset());
    }


    private CharacterState currentCharacterState = CharacterState.Player;

    public CharacterState GetCurrentCharacterState()
    {
        return currentCharacterState;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Tutorial");
    }

    IEnumerator DeathAnimationReset()
    {
        // Wait for a short delay before resetting
        yield return new WaitForSeconds(delay);

        // Reset isAlive and isDead
        isAlive = true;

        // Trigger scene reload
        characterSwitcher.Respawn();
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

        // Quits the game
        Application.Quit();
    }
}
