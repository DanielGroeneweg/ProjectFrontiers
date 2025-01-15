using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CheckMoney : MonoBehaviour
{
    public UnityEvent hasEnough;
    public UnityEvent insufficientFunds;
    public void Check(int moneyRequired)
    {
        if (PlayerPrefs.GetInt("Money") >= moneyRequired) hasEnough?.Invoke();
        else insufficientFunds?.Invoke();
    }
}
