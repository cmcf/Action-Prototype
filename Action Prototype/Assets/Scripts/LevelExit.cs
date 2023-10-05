using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    void OnTriggerEnter2D(Collider2D collision)
    { 
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        // Gets current level
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Sets next level
        int nextSceneIndex = currentSceneIndex + 1;

        // Loads first level if the player has completed all levels
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(currentSceneIndex);
    }
}
