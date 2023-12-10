using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Abertay.Analytics;

public class LockedDoor : MonoBehaviour
{
    Animator anim;
    float loadDelay = 1f;

    void Start()
    {
        anim= GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameSession.Instance.keysCollected >= 2)
        {
            anim.SetTrigger("IsOpen");
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        /* Old data collection event
         * Saves amount of times the player has switched each level
        Dictionary<string, object> switchData = new Dictionary<string, object>();
        switchData.Add("timesPlayerHasSwitched", characterSwitcher.timesPlayerHasSwitched);
        AnalyticsManager.SendCustomEvent("TimesSwitched", switchData);
        */

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
