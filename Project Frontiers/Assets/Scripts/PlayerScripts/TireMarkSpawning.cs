using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(PointsManager))]
public class TireMarkSpawning : MonoBehaviour
{
    [SerializeField] private PointsManager pointsManager;
    public TrailRenderer[] tireMarks;

    // Internal
    [DoNotSerialize] public bool tireMarksFlags = false;
    private void Update()
    {
        if (pointsManager.isDrifting) StartEmitter();
        else StopEmitter();
    }
    private void StartEmitter()
    {
        if (tireMarksFlags) return;
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = true;
        }

        tireMarksFlags = true;
    }
    private void StopEmitter()
    {
        if (!tireMarksFlags) return;
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = false;
        }

        tireMarksFlags = false;
    }
}