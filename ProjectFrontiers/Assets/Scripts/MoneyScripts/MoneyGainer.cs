using UnityEngine;
public class MoneyGainer : MonoBehaviour
{
    public void GainMoney(int money)
    {
        PlayerPrefs.SetInt("Money", (PlayerPrefs.GetInt("Money") + money));
    }
}