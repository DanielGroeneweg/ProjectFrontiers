using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class DisplayHighScore : MonoBehaviour
{
    [SerializeField] private TMP_Text highscoreDisplay;
    private void Start()
    {
        float highscore = Mathf.Floor((PlayerPrefs.GetInt("HighScore") / 1000f) * 10.0f) * 0.1f;
        highscoreDisplay.text = highscore.ToString() + "K";
    }
}
