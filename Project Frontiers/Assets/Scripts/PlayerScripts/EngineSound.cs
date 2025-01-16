using UnityEngine;
[RequireComponent(typeof(CarControl))]
[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private CarControl carControl;
    [SerializeField] private AudioSource engineSound;

    [Header("Sound Stats")]
    [SerializeField] private float pitchMin;
    [SerializeField] private float pitchMax;
    private void Update()
    {
        float diff = pitchMax - pitchMin;
        float percentage = carControl.SpeedFactor();

        // If the player is not standing still
        if (percentage > 0)
        {
            // Cange the pitch according to the speed while the engine sound is playing
            if (engineSound.isPlaying) engineSound.pitch = pitchMin + diff * percentage;

            // Start playing the engine sound
            else
            {
                engineSound.pitch = pitchMin;
                engineSound.Play();
            }
        }

        // Stop playing the engine sound if the player is standing still
        else if (engineSound.isPlaying) engineSound.Stop();
    }
}
