using UnityEngine;
public class CarControl : MonoBehaviour
{
    #region variables
    // In Inspector
    [Header("Required  Components")]
    public WheelControl[] wheels;
    [SerializeField] private Rigidbody rigidBody;

    [Header("Stats")]
    [SerializeField] private float motorTorque = 2000;
    [SerializeField] private float brakeTorque = 2000;
    [SerializeField] private float passiveBrakeTorque = 500;
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float steeringRange = 30;
    [SerializeField] private float steeringRangeAtMaxSpeed = 10;
    [SerializeField] private float centreOfGravityOffset = -1f;
    [SerializeField] private float handbrakeSteeringRangeModifier = 2;
    [SerializeField] private float handbrakeBrakeTorqueModifier = 2;
    [SerializeField] private float groundOffSet = 0.1f;

    [Header("Upgrade Stats")]
    [SerializeField] private float accelerationIncrease;
    [SerializeField] private float maxSpeedIncrease;
    [SerializeField] private float brakesIncrease;

    // Internal
    private float vInput;
    private float hInput;
    private bool handBrake;
    private bool playerControls = true;
    #endregion

    #region UnityMethods
    void Start()
    {
        // Adjust center of mass vertically, to help prevent the car from rolling
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        // Apply the store upgrades for the car to the car
        ApplyUpgrades();
    }
    private void Update()
    {
        GetPlayerInputs();
    }
    void FixedUpdate()
    {
        DoCarPhysics();
    }
    #endregion

    #region CarPhysics
    private void DoCarPhysics()
    {
        // Check whether the user input is in the same direction as the car's velocity
        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(ForwardSpeed());
        if (vInput == 0) isAccelerating = false;

        foreach (var wheel in wheels)
        {
            // Apply steering to Wheel colliders that have "Steerable" enabled
            if (wheel.steerable)
            {
                float modifier = 1;
                if (handBrake)
                {
                    modifier = handbrakeSteeringRangeModifier;
                }
                wheel.WheelCollider.steerAngle = hInput * CurrentSteerRange() * modifier;
            }

            if (isAccelerating)
            {
                // Apply torque to Wheel colliders that have "Motorized" enabled
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = vInput * CurrentMotorTorque();
                }
                wheel.WheelCollider.brakeTorque = 0;
            }

            else
            {
                // If the user is trying to go in the opposite direction apply brakes to all wheels
                if (vInput < 0 || vInput > 0)
                {
                    float modifier = 1;
                    if (handBrake) modifier = handbrakeBrakeTorqueModifier;
                    wheel.WheelCollider.brakeTorque = brakeTorque * modifier;
                }

                // Passively slow down the car if the player is not accelerating and not using the handbrake
                else if (vInput == 0 && !handBrake)
                {
                    wheel.WheelCollider.brakeTorque = passiveBrakeTorque;
                }

                // Quickly slow down the car if the player is not acceleation but is using the handbrake
                else if (vInput == 0 && handBrake)
                {
                    wheel.WheelCollider.brakeTorque = brakeTorque * handbrakeBrakeTorqueModifier;
                }

                wheel.WheelCollider.motorTorque = 0;
            }
        }
    }
    #endregion

    #region PlayerInput
    public void DisablePlayerControls()
    {
        playerControls = false;
    }
    private void GetPlayerInputs()
    {
        if (playerControls)
        {
            vInput = Input.GetAxis("Vertical");
            hInput = Input.GetAxis("Horizontal");
            handBrake = Input.GetKey(KeyCode.Space);
        }
        
        else
        {
            vInput = 0;
            hInput = 0;
            handBrake = false;
        }
    }
    #endregion

    #region Upgrades
    private void ApplyUpgrades()
    {
        // Acceleration
        if (PlayerPrefs.GetInt("AccelerationTier") > 1) motorTorque += (PlayerPrefs.GetInt("AccelerationTier") - 1) * accelerationIncrease;

        // Max Speed
        if (PlayerPrefs.GetInt("EngineTier") > 1) maxSpeed += (PlayerPrefs.GetInt("EngineTier") - 1) * maxSpeedIncrease;

        // Brakes
        if (PlayerPrefs.GetInt("BrakesTier") > 1) brakeTorque += (PlayerPrefs.GetInt("BrakesTier") - 1) * brakesIncrease;

        // Spoiler (WIP)
    }
    #endregion

    #region Functions
    // Calculate current speed in relation to the forward direction of the car
    // (this returns a negative number when traveling backwards)
    public float ForwardSpeed()
    {
        return Vector3.Dot(transform.forward, rigidBody.velocity);
    }

    // Calculate how close the car is to top speed as a number from zero to one
    public float SpeedFactor()
    {
        if (ForwardSpeed() >= 0) return Mathf.InverseLerp(0, maxSpeed, ForwardSpeed());
        else if (ForwardSpeed() < 0) return Mathf.InverseLerp(0, -maxSpeed, ForwardSpeed());
        return 0;
    }

    // Use the speed factor to calculate how much torque is available 
    // (zero torque at top speed)
    private float CurrentMotorTorque()
    {
        return Mathf.Lerp(motorTorque, 0, SpeedFactor());
    }

    // use the speed factor to calculate how much to steer 
    // (the car steers more gently at top speed)
    private float CurrentSteerRange()
    {
        return Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, SpeedFactor());
    }

    // Checks if the car is touching the ground with 4 wheels
    public bool isOnGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundOffSet))
        {
            if (hit.collider.tag == "Road") return true;
            else return false;
        }
        else return false;
    }
    #endregion
}