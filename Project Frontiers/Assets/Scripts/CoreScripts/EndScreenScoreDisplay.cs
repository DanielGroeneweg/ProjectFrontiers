using TMPro;
using UnityEngine;
public class EndScreenScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text secondsLeftDisplay;
    [SerializeField] private TMP_Text timeToScoreDisplay;
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text scoreToMoneyDisplay;
    [SerializeField] private TMP_Text totalEarningsDisplay;
    [SerializeField] private int moneyForCompleting = 500;

    private void Start()
    {
        secondsLeftDisplay.text = PlayerPrefs.GetInt("SecondsLeft").ToString();
        timeToScoreDisplay.text = PlayerPrefs.GetInt("TimeLeftScore").ToString();
        scoreDisplay.text = PlayerPrefs.GetInt("Score").ToString();
        scoreToMoneyDisplay.text = PlayerPrefs.GetInt("ScoreToMoney").ToString();
        totalEarningsDisplay.text = (PlayerPrefs.GetInt("ScoreToMoney") + moneyForCompleting).ToString();
    }
}
