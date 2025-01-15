using UnityEngine;
public class UpgradesManager : MonoBehaviour
{
    void Start()
    {
        //Upgrades
        PlayerPrefs.GetInt("AccelaretionTier", 1);
        PlayerPrefs.GetInt("SpoilerTier", 1);
        PlayerPrefs.GetInt("BrakesTier", 1);
    }

    public void UpgradeStat(string upgrade)
    {
        PlayerPrefs.SetInt(upgrade, PlayerPrefs.GetInt(upgrade + 1));
    }
}
