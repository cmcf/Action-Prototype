using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Abertay.Analytics;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    float finalTime;
 
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
            // Stops the timer
            Timer.Instance.StopTime();
            finalTime = Timer.Instance.timer;
            Dictionary<string, object> playerTime = new Dictionary<string, object>();
            playerTime.Add("playerFinalTime", finalTime);
            AnalyticsManager.SendCustomEvent("PlayerTime", playerTime);
        }

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
