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

        Dictionary<string, object> deathData = new Dictionary<string, object>();
        deathData.Add("timesPlayerHasDied", timesPlayerHasDied);
        AnalyticsManager.SendCustomEvent("TotalDeaths", deathData);


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

    public static void Quit()
    {
        Application.Quit();
    }
}
