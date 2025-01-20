using UnityEngine;
public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform firstCheckpoint;
    private Transform savedCheckPoint;
    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ResetToLastCheckpoint();
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
        // Reset the car's position and rotation
        transform.position = savedCheckPoint.position;
        transform.rotation = savedCheckPoint.rotation;

        // Reset car speed
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}