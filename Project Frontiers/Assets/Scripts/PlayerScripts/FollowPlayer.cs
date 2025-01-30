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
            player.localEulerAngles.x - rotationChange.x,
            player.localEulerAngles.y,
            player.localEulerAngles.z - rotationChange.z);

        oldAngles = player.localEulerAngles;
    }
}