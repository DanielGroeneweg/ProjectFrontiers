using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class PlayTimer : MonoBehaviour
{
    // In Inspector
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text timeDisplay;
    [SerializeField] private float startMinutes;
    [SerializeField] private float startSeconds;
    public UnityEvent TimeRanOut;

    // Internal
    private float startTime;
    private float time;
    private bool timerPlaying;
    private float seconds;
    private float minutes;
    private void Start()
    {
        timerPlaying = true;
        seconds = startSeconds;
        minutes = startMinutes;

        time = startSeconds + startMinutes * 60;
        startTime = time;
    }
    private void Update()
    {
        if  (timerPlaying) DoTimer();
        DisplayTimer();
        StormProgress();
    }
    private void DoTimer()
    {
        if (Mathf.Floor(seconds) < 0)
        {
            seconds = 60;
            minutes--;
        }

        seconds -= Time.deltaTime;
        time -= Time.deltaTime;

        if (minutes <= 0 && seconds <= 0) TimeRanOut?.Invoke();
    }
    private void DisplayTimer()
    {
        if (minutes < 10)
        {
            if (seconds < 10) timeDisplay.text = "0" + ((int)minutes).ToString() + ":0" + ((int)seconds).ToString();
            else timeDisplay.text = "0" + ((int)minutes).ToString() + ":" + ((int)seconds).ToString();

        }

        else
        {
            if (seconds < 10) timeDisplay.text = ((int)minutes).ToString() + ":0" + ((int)seconds).ToString();
            else timeDisplay.text = ((int)minutes).ToString() + ":" + ((int)seconds).ToString();
        }
    }
    private void StormProgress()
    {
        float diff = 1f / startTime;
        float secondsPlayed = startTime - time;
        float fill = diff * secondsPlayed;
        fillImage.fillAmount = fill;
    }
    public void StopTimer()
    {
        timerPlaying = false;
    }
    public float GetSecondsLeft()
    {
        return Mathf.Floor(seconds + minutes * 60);
    }
}