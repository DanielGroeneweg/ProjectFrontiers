using UnityEngine;
public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform firstCheckpoint;
    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private CarControl carControl;
    [SerializeField] private TireMarkSpawning tireMarkSpawning;

    // Internal
    private Transform savedCheckPoint;
    private bool canReset;
    private void Start()
    {
        savedCheckPoint = firstCheckpoint;
    }
    private void Update()
    {
        if (canReset && Input.GetKeyDown(KeyCode.R))
        {
            ResetToLastCheckpoint();
            pointsManager.StopOnRespawn();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            SetCheckPoint(other.transform);
        }
    }
    private void SetCheckPoint(Transform checkpoint)
    {
        savedCheckPoint = checkpoint;
    }
    private void ResetToLastCheckpoint()
    {
        // Reset trailmarks
        foreach (TrailRenderer T in tireMarkSpawning.tireMarks)
        {
            T.emitting = false;
            T.Clear();
        }
        tireMarkSpawning.tireMarksFlags = false;

        // Reset car speed
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        foreach (WheelControl wheel in carControl.wheels)
        {
            wheel.WheelCollider.motorTorque = 0;
            wheel.WheelCollider.brakeTorque = 0;
        }

        // Reset the car's position and rotation
        transform.position = savedCheckPoint.position;
        transform.rotation = savedCheckPoint.rotation;
    }

    public void DisableResetting()
    {
        canReset = false;
    }
}