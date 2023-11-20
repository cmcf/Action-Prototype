using Abertay.Analytics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }
 
    public ScenePersist scenePersist;

    public int lives = 3;
    public int keysCollected = 0;
    public bool isAlive = true;
    int timesPlayerHasDied = 0;

    float loadLevelDelay = 0.8f;

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
        Invoke("ReloadScene", loadLevelDelay);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ReloadScene()
    {
      
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isAlive = true;
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
