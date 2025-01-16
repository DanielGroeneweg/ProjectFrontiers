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

        if (percentage > 0)
        {
            if (engineSound.isPlaying) engineSound.pitch = pitchMin + diff * percentage;
            else
            {
                engineSound.pitch = pitchMin;
                engineSound.Play();
            }
        }

        else if (engineSound.isPlaying) engineSound.Stop();
    }
}
