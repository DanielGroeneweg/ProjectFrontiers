using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PointsManager))]
public class TireScreechSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioClip;
    [SerializeField] private PointsManager pointsManager;

    bool tireScreechFlag = false;
    private void Update()
    {
        if (pointsManager.isDrifting)
        {
            if (tireScreechFlag) return;

            audioClip.Play();
            tireScreechFlag = true;
        }

        else
        {
            if (!tireScreechFlag) return;

            audioClip.Stop();
            tireScreechFlag = false;
        }
    }
}