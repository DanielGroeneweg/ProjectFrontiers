using UnityEngine;
public class MoneyGainer : MonoBehaviour
{
    private int money;
    void Start()
    {
        money = PlayerPrefs.GetInt("Money");
    }
    public void MoneyGain(int moneyIncrease)
    {
        money = PlayerPrefs.GetInt("Money") + moneyIncrease;
        PlayerPrefs.SetInt("Money", money);
    }
}