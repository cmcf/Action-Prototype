using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Abertay.Analytics;

public class LockedDoor : MonoBehaviour
{
    Animator anim;
    float loadDelay = 1f;
    CharacterSwitcher characterSwitcher;

    void Start()
    {
        anim= GetComponent<Animator>();
        characterSwitcher = FindObjectOfType<CharacterSwitcher>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameSession.Instance.keysCollected >= 2)
        {
            Debug.Log("Open door");
            anim.SetTrigger("IsOpen");
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
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
