using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance; // Singleton instance
    public Slider timerSlider;
    GameSession gameSession;

    [SerializeField] float currentTime;
    [SerializeField] float maxTime = 120;

    [SerializeField] float timeDelay = 0.001f;

    public bool stopTimer = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currentTime = maxTime;
        timerSlider.maxValue = currentTime;
        timerSlider.value = currentTime;
        StartTimer();
    }

    public void StartTimer()
    {
        gameSession = FindAnyObjectByType<GameSession>();
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (!stopTimer)
        {
            if (gameSession.isAlive)
            {
                // Decreases slider time
                currentTime -= Time.deltaTime;
                yield return new WaitForSeconds(timeDelay);

                if (currentTime <= 0)
                {
                    stopTimer = true;
                }
                // Updates slider value
                if (!stopTimer)
                {
                    if (timerSlider != null)
                    {
                        timerSlider.value = currentTime;
                    }
                    
                }
            }
        }
    }

    public void StopTime()
    {
        stopTimer = true;
    }

    public void IncreaseTime()
    {
        // Check if increasing totalTime by 1 will not exceed max
        if (currentTime + 1 <= maxTime)
        {
            currentTime++;
        }
    }
}
