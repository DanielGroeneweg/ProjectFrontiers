using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class DisplayHighScore : MonoBehaviour
{
    [SerializeField] private TMP_Text highscoreDisplay;
    private void Start()
    {
        highscoreDisplay.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
