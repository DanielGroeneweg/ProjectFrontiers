using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    [Serializable] public class Upgrade
    {
        public string upgradeName;
        public float upgradeTierMax;
        public GameObject purchaseButton;
        public GameObject purchaseMenu;
        public TMP_Text tierDisplay;
    }

    [Header("Required Components")]
    [SerializeField] public Upgrade[] upgrades;
    void Start()
    {
        //Upgrades
        /*
        PlayerPrefs.SetInt("AccelerationTier", 1);
        PlayerPrefs.SetInt("SpoilerTier", 1);
        PlayerPrefs.SetInt("BrakesTier", 1);
        PlayerPrefs.SetInt("EngineTier", 1);
        PlayerPrefs.SetInt("Money", 0);
        */
    }

    public void UpgradeStat(string upgrade)
    {
        PlayerPrefs.SetInt(upgrade, PlayerPrefs.GetInt(upgrade) + 1);
    }

    private void Update()
    {
        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.purchaseMenu.activeSelf)
            {
                // Display upgrade tier
                upgrade.tierDisplay.text = PlayerPrefs.GetInt(upgrade.upgradeName).ToString();

                // Disable purchasing after max tier has been reached
                if (PlayerPrefs.GetInt(upgrade.upgradeName) == upgrade.upgradeTierMax && upgrade.purchaseButton.activeSelf)
                    upgrade.purchaseButton.SetActive(false);

                // Enable it if it's somehow disabled while it should be enabled
                else if (PlayerPrefs.GetInt(upgrade.upgradeName) < upgrade.upgradeTierMax && !upgrade.purchaseButton.activeSelf)
                    upgrade.purchaseButton.SetActive(true);
            }
        }
    }
}
