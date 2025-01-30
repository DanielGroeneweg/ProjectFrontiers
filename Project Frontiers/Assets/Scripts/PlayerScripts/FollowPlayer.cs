using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Internal
    private Vector3 oldAngles;
    private void Start()
    {
        oldAngles = player.localEulerAngles;
    }
    private void Update()
    {
        Vector3 rotationChange = player.localEulerAngles - oldAngles;

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x - rotationChange.x,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z - rotationChange.z);

        oldAngles = player.localEulerAngles;
    }
}