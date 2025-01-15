using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class Money : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private int money;

    // All upgrades:
    private int accelerationTier = 1;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("Money", 0);
        money = PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("AccelerationTier", 1);
        accelerationTier = PlayerPrefs.GetInt("AccelerationTier");
    }

    // Update is called once per frame
    void Update()
    {
        text.text = money.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int temp = PlayerPrefs.GetInt("Money");
            temp += 10;
            money = temp;
            PlayerPrefs.SetInt("Money", money);
        }
        
    }
}
