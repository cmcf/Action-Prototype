using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider timerSlider;

    [SerializeField] float totalTime = 50f;

    [SerializeField] float currentTime;
    [SerializeField] float timeDelay = 0.001f;

    public bool stopTimer = false;
    void Start()
    {
        timerSlider.maxValue = totalTime;
        timerSlider.value = totalTime;
        StartTimer();
    }

    void StartTimer()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (!stopTimer)
        {
            // Decreases slider time
            totalTime -= Time.deltaTime;
            yield return new WaitForSeconds(timeDelay);

            if (totalTime <= 0)
            {
                stopTimer = true;
            }
            // Updates slider value
            if (!stopTimer)
            {
                timerSlider.value = totalTime;
            }
        }
        Debug.Log("Out of time");
        
    }

    void StopTime()
    {
        stopTimer = true;
    }

    public void IncreaseTime()
    {
        // Check if increasing totalTime by 1 will not exceed max
        if (totalTime + 1 <= 50)
        {
            totalTime++;
        }
    }
}
