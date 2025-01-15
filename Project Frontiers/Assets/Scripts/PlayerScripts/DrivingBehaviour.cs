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
    [SerializeField] private float handbrakeDeacceleration;
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

        // Handbrake
        if (pressedSpace)
        {
            // When going forward
            if (velocity > 0)
            {
                rb.AddForce(-transform.forward * handbrakeDeacceleration);
                velocity -= handbrakeDeacceleration;
            }

            // When going backward
            else if (velocity < 0)
            {
                rb.AddForce(transform.forward * handbrakeDeacceleration);
                velocity += handbrakeDeacceleration;

                // Make it stop
                if (velocity > -0.5)
                {
                    rb.velocity = Vector3.zero;
                    velocity = 0;
                }
            }
        }

        // Calculate drag the way unity engine does it
        velocity *= 1 - Time.deltaTime * rb.drag;
    }

    private void DoSteering()
    {
        //Handbrakes
        float modifier = 1;
        if (pressedSpace) modifier = spaceRotationSpeedModifier;

        // Steering While driving forward
        if (velocity > 0)
        {
            if (pressedA) rb.AddTorque(-transform.up * rotationSpeed * modifier);
            if (pressedD) rb.AddTorque(transform.up * rotationSpeed * modifier);
        }

        // Stering while driving backward
        else if (velocity < 0)
        {
            if (pressedA) rb.AddTorque(transform.up * rotationSpeed * modifier);
            if (pressedD) rb.AddTorque(-transform.up * rotationSpeed * modifier);
        }
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
