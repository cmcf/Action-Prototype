using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameSession : MonoBehaviour
{
    public int lives = 3;
    public void PlayerHit()
    {
        TakeLife();
        Debug.Log("Lives reduced");
    }

    // Decreases player lives by 1 and reloads current level
    void TakeLife()
    {
        lives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
