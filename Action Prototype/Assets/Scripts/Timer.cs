using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance; // Singleton instance

    public TextMeshProUGUI timerMins;
    public TextMeshProUGUI timerMins2;
    public TextMeshProUGUI timerSeconds;
    public TextMeshProUGUI timerSeconds2;

    public float timer = 0f;
    private bool stopTimer = false;

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

    private void Update()
    {
        if (!stopTimer)
        {
            timer += Time.deltaTime;

            // Prevent timer from going below 0
            timer = Mathf.Max(timer, 0f);

            UpdateDisplay(timer);
        }
    }

    private void UpdateDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        // Displays time in time format
        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        timerMins.text = currentTime[0].ToString();
        timerMins2.text = currentTime[1].ToString();
        timerSeconds.text = currentTime[2].ToString();
        timerSeconds2.text = currentTime[3].ToString();
    }


    public void StopTime()
    {
        stopTimer = true;
    }
    public void DecreaseTime()
    {
        // Decreases time
        timer--;
    }
}
