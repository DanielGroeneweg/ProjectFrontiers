using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private CarControl carControl;
    [SerializeField] private float maxSpeedInKMH;

    private float modifier;
    private void Start()
    {
        modifier = maxSpeedInKMH / carControl.maxSpeed;
    }
    void Update()
    {
        int speed = (int)Mathf.Round(Mathf.Clamp(Mathf.Abs(carControl.ForwardSpeed()), 0, carControl.maxSpeed) * modifier);
        speedText.text = speed.ToString();

        float speedFactor = carControl.SpeedFactor();
        float fill = Mathf.Floor(speedFactor / 0.02f) * 0.02f;
        fillImage.fillAmount = Mathf.Clamp(fill, 0, 1);
    }
}
