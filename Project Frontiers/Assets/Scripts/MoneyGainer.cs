using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]

public class MoneyGainer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private int money;

    void Start()
    {
        money = PlayerPrefs.GetInt("Money");
    }
    public void MoneyGain()
    {
        text.text = money.ToString();

        money = PlayerPrefs.GetInt("Money");
        money += 10;
        PlayerPrefs.SetInt("Money", money);
    }
}
