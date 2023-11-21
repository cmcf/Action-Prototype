using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Abertay.Analytics;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    CharacterSwitcher characterSwitcher;
    float finalTime;

    [System.Obsolete]
    private void Start()
    {
        characterSwitcher = FindObjectOfType<CharacterSwitcher>();

    }
    void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player") || collision.CompareTag("Dog"))
        {
            StartCoroutine(LoadNextLevel());
        }            
    }

    public IEnumerator LoadNextLevel()
    {
        if (Timer.Instance != null)
        {
            Debug.Log(characterSwitcher.timesPlayerHasSwitched);
            // Stops the timer
            Timer.Instance.StopTime();
            finalTime = Timer.Instance.timer;
            Dictionary<string, object> playerTime = new Dictionary<string, object>();
            playerTime.Add("playerFinalTime", finalTime);
            AnalyticsManager.SendCustomEvent("PlayerTime", playerTime);

        }
        // Saves amount of times the player has switched each level
        Dictionary<string, object> switchData = new Dictionary<string, object>();
        switchData.Add("timesPlayerHasSwitched", characterSwitcher.timesPlayerHasSwitched);
        AnalyticsManager.SendCustomEvent("TimesSwitched", switchData);

        Debug.Log(characterSwitcher.timesPlayerHasSwitched);

        // Loads level after a delay
        yield return new WaitForSecondsRealtime(loadDelay);
        // Gets current level
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Sets next level
        int nextSceneIndex = currentSceneIndex + 1;

        // Resets next level if player has reached the last level
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {

            nextSceneIndex = 0;
        }
        // Loads next level
        SceneManager.LoadScene(nextSceneIndex);
    }
}
