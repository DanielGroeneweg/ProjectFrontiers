using UnityEngine;

public class WheelControl : MonoBehaviour
{
    // In Inspector
    [Header("Required Components")]
    [SerializeField] private Transform wheelModel;
    public WheelCollider WheelCollider;

    [Header("Stats")]
    public bool steerable;
    public bool motorized;

    // Not In Inspector
    private Vector3 position;
    private Quaternion rotation;
    void FixedUpdate()
    {
        // Get the Wheel collider's world pose values and
        // use them to set the wheel model's position and rotation
        WheelCollider.GetWorldPose(out position, out rotation);
        wheelModel.transform.position = position;
        wheelModel.transform.rotation = rotation;
    }
}