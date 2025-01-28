using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Image fillImage; // UI Image for Fill Amount
    public Rigidbody carRigidbody; // Reference to the car's Rigidbody
    public float maxSpeed = 200f; // Set max speed of the car

    void Update()
    {
        // Get current speed (magnitude of velocity)
        float currentSpeed = carRigidbody.velocity.magnitude; // Convert m/s to km/h

        // Normalize speed (0 to 1) and set fill amount
        fillImage.fillAmount = Mathf.Clamp01(currentSpeed / maxSpeed);
    }
}
