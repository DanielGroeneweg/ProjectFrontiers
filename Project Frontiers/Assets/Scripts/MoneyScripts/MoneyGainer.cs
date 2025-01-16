using UnityEngine;
public class MoneyGainer : MonoBehaviour
{
    [SerializeField] private int moneyIncrease;
    private int money;
    void Start()
    {
        money = PlayerPrefs.GetInt("Money");
    }
    public void MoneyGain()
    {
        money = PlayerPrefs.GetInt("Money") + moneyIncrease;
        PlayerPrefs.SetInt("Money", money);
    }
}