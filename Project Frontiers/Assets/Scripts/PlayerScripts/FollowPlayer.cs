using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    // Internal
    private Vector3 oldAngles;
    private void Start()
    {
        oldAngles = transform.localEulerAngles;
    }
    private void Update()
    {
        Vector3 rotationChange = transform.localEulerAngles - oldAngles;

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x - rotationChange.x,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z - rotationChange.z);

        oldAngles = transform.localEulerAngles;
    }
}