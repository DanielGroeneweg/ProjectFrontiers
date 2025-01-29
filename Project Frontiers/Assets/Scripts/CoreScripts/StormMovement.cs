using UnityEngine;
public class StormMovement : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float speed;
    void FixedUpdate()
    {
        transform.position += (moveDirection.normalized * speed);
    }
}
