using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerCar;

    // Internal
    private Vector3 oldPosition;
    private void Update()
    {
        Vector3 movementChange = playerCar.position - oldPosition;

        transform.position += movementChange;

        oldPosition = playerCar.position;
    }
}