using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetUpgrades : MonoBehaviour
{
    // Start is called before the first frame update
    public void Resetting()
    {
        PlayerPrefs.SetInt("AccelerationTier", 1);
        PlayerPrefs.SetInt("SpoilerTier", 1);
        PlayerPrefs.SetInt("BrakesTier", 1);
        PlayerPrefs.SetInt("EngineTier", 1);
    }
}