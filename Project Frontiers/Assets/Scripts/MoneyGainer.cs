using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGainer : MonoBehaviour
{
    public void MoneyGain()
    {
        int temp = PlayerPrefs.GetInt("Money");
        temp += 10;
        PlayerPrefs.SetInt("Money", temp);
    }
}
