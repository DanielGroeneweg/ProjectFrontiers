using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class DrivingBehaviour : MonoBehaviour
{
    #region InUnityInspector
    [Header("Required Components")]
    [SerializeField] private Rigidbody rb;

    [Header("Car Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxReverseSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float spaceRotationSpeedModifier;
    #endregion

    #region InternalVariables
    // Player Input
    private bool pressedW;
    private bool pressedA;
    private bool pressedS;
    private bool pressedD;
    private bool pressedSpace;

    // Physics
    private float velocity;
    #endregion

    #region UnityMethods
    private void Update()
    {
        GetInputBools();
    }

    private void FixedUpdate()
    {
        DoAcceleration();
        DoSteering();
        ResetInputBools();
    }
    #endregion

    #region PlayerControls
    private void DoAcceleration()
    {
        // Forward
        if (pressedW && velocity < maxSpeed)
        {
            rb.AddForce(transform.forward * speed);
            velocity += speed;
        }

        // Backward/Breaking
        if (pressedS && velocity > -maxReverseSpeed)
        {
            rb.AddForce(-transform.forward * speed);
            velocity -= speed;
        }

        if (pressedSpace)
        {

        }

        // Calculate drag the way unity engine does it
        velocity *= 1 - Time.deltaTime * rb.drag;
    }

    private void DoSteering()
    {
        float modifier = 1;
        if (pressedSpace) modifier = spaceRotationSpeedModifier;

        if (pressedA) rb.angularVelocity -= new Vector3(0, 1, 0) * rotationSpeed * modifier;
        if (pressedD) rb.angularVelocity += new Vector3(0, 1, 0) * rotationSpeed * modifier;
    }
    #endregion

    #region PlayerInput
    private void GetInputBools()
    {
        if (Input.GetKey(KeyCode.W)) pressedW = true;
        if (Input.GetKey(KeyCode.A)) pressedA = true;
        if (Input.GetKey(KeyCode.S)) pressedS = true;
        if (Input.GetKey(KeyCode.D)) pressedD = true;
        if (Input.GetKey(KeyCode.Space)) pressedSpace = true;
    }
    private void ResetInputBools()
    {
        pressedW = false;
        pressedA = false;
        pressedS = false;
        pressedD = false;
        pressedSpace = false;
    }
    #endregion
}
