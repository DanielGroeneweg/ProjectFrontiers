using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyDisplay;
    private int money;
    void Update()
    {
        moneyDisplay.text = PlayerPrefs.GetInt("Money").ToString();
    }
}