using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PointsManager))]
public class TireScreechSoundPlayer : MonoBehaviour
{
    [SerializeField] private GameObject tireScreechAudioObject;
    [SerializeField] private PointsManager pointsManager;

    bool tireScreechFlag = false;
    private void Update()
    {
        if (pointsManager.isDrifting)
        {
            if (tireScreechFlag) return;

            tireScreechAudioObject.SetActive(true);
            tireScreechFlag = true;
        }

        else
        {
            if (!tireScreechFlag) return;

            tireScreechAudioObject.SetActive(false);
            tireScreechFlag = false;
        }
    }
}