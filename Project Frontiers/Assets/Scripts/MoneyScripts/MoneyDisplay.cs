using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyDisplay;
    private int money;
    private void Start()
    {
        money = PlayerPrefs.GetInt("Money");
    }
    void Update()
    {
        moneyDisplay.text = money.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            money = PlayerPrefs.GetInt("Money") + 5000;
            PlayerPrefs.SetInt("Money", money);
        }
    }
}
