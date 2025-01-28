using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private CarControl carControl;
    void Update()
    {
        fillImage.fillAmount = carControl.SpeedFactor();
    }
}
