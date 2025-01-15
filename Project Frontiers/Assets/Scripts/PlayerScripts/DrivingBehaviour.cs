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
    private float oldVel;
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
        HandleVelocity();
        ResetInputBools();
    }
    #endregion

    #region PlayerControls
    private void HandleVelocity()
    {
        float dif = rb.velocity.magnitude - oldVel;

        if (pressedW)
        {
            if (dif > 0)
            {
                velocity += dif;
            }

            else if (dif < 0)
            {
                velocity += dif;
            }
        }

        else if (pressedS)
        {
            if (dif > 0)
            {
                velocity -= dif;
            }

            else if (dif < 0)
            {
                velocity += dif;
            }
        }

        else if (dif < 0) velocity += dif;
        else if (dif > 0) velocity += dif;

        if (rb.velocity.magnitude == 0) velocity = 0;

        oldVel = rb.velocity.magnitude;

        Debug.Log("Dif: " + dif + " velocity: " + velocity + " rb velocity: " + rb.velocity.magnitude);
    }
    private void DoAcceleration()
    {
        // Forward
        if (pressedW && velocity < maxSpeed)
        {
            rb.AddForce(transform.forward * speed);
        }

        // Backward/Breaking
        if (pressedS && velocity > -maxReverseSpeed)
        {
            rb.AddForce(-transform.forward * speed);
        }

        // Handbrake
        if (pressedSpace)
        {
            // When going forward
            if (velocity > 0)
            {
                rb.AddForce(-transform.forward * handbrakeDeacceleration);
            }

            // When going backward
            else if (velocity < 0)
            {
                rb.AddForce(transform.forward * handbrakeDeacceleration);

                // Make it stop
                if (velocity > -0.5)
                {
                    rb.velocity = Vector3.zero;
                }
            }
        }
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