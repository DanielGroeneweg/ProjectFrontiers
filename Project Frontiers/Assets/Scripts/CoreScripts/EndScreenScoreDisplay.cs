using TMPro;
using UnityEngine;
public class EndScreenScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text secondsLeftDisplay;
    [SerializeField] private TMP_Text timeToScoreDisplay;
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text scoreToMoneyDisplay;

    private void Start()
    {
        secondsLeftDisplay.text = PlayerPrefs.GetInt("SecondsLeft").ToString();
        timeToScoreDisplay.text = PlayerPrefs.GetInt("TimeLeftScore").ToString();
        scoreDisplay.text = PlayerPrefs.GetInt("Score").ToString();
        scoreToMoneyDisplay.text = PlayerPrefs.GetInt("ScoreToMoney").ToString();
    }
}
