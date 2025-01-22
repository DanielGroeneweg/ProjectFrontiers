using UnityEngine;
[RequireComponent(typeof(PointsManager))]
public class SmokeParticleSpawning : MonoBehaviour
{
    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private ParticleSystem[] particles;

    // Internal
    private bool particleFlags = false;
    private void Update()
    {
        if (pointsManager.isDrifting) StartEmitter();
        else StopEmitter();
    }
    private void StartEmitter()
    {
        if (particleFlags) return;
        foreach (ParticleSystem P in particles)
        {
            P.Play();
        }

        particleFlags = true;
    }
    private void StopEmitter()
    {
        if (!particleFlags) return;
        foreach (ParticleSystem P in particles)
        {
            P.Stop();
        }

        particleFlags = false;
    }
}