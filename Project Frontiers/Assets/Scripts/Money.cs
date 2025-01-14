using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class Money : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public int money;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("Money", 0);
        money = PlayerPrefs.GetInt("Money");
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
