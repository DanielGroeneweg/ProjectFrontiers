using TMPro;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(TMP_Text))]
public class PlayTimer : MonoBehaviour
{
    // In Inspector
    [SerializeField] private TMP_Text timeDisplay;
    [SerializeField] private float startMinutes;
    [SerializeField] private float startSeconds;
    public UnityEvent TimeRanOut;

    // Internal
    private float seconds;
    private float minutes;
    private void Start()
    {
        seconds = startSeconds;
        minutes = startMinutes;
    }
    private void Update()
    {
        DoTimer();
        DisplayTimer();
    }
    private void DoTimer()
    {
        if (Mathf.Floor(seconds) < 0)
        {
            seconds = 60;
            minutes--;
        }

        seconds -= Time.deltaTime;

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
}