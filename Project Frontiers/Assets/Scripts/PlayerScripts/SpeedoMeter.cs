using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private CarControl carControl;
    void Update()
    {
        speedText.text = Mathf.Round(Mathf.Clamp(Mathf.Abs(carControl.ForwardSpeed()), 0, carControl.maxSpeed)).ToString();
        fillImage.fillAmount = carControl.SpeedFactor();
    }
}
