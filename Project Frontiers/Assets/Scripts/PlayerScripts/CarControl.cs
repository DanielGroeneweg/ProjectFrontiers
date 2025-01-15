using UnityEngine;

public class CarControl : MonoBehaviour
{
    #region variables
    // In Inspector
    [Header("Required  Components")]
    [SerializeField] WheelControl[] wheels;
    [SerializeField] Rigidbody rigidBody;

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

    [Header("Upgrade Stats")]
    [SerializeField] private float accelerationIncrease;
    [SerializeField] private float maxSpeedIncrease;
    [SerializeField] private float brakesIncrease;

    // Internal
    float vInput;
    float hInput;
    bool handBrake;
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
                if (handBrake) modifier = handbrakeSteeringRangeModifier;
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
    private void GetPlayerInputs()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        handBrake = Input.GetKey(KeyCode.Space);
    }
    #endregion

    #region Upgrades
    private void ApplyUpgrades()
    {
        // Acceleration
        if (PlayerPrefs.GetInt("AccelerationTier") > 1) motorTorque += (PlayerPrefs.GetInt("AccelerationTier") - 1) * accelerationIncrease;

        // Max Speed
        if (PlayerPrefs.GetInt("EngineTier") > 1) maxSpeed += (PlayerPrefs.GetInt("Engine") - 1) * maxSpeedIncrease;

        // Brakes
        if (PlayerPrefs.GetInt("BrakesTier") > 1) brakeTorque += (PlayerPrefs.GetInt("BrakesTier") - 1) * brakesIncrease;

        // Spoiler (WIP)
    }
    #endregion

    #region Functions
    // Calculate current speed in relation to the forward direction of the car
    // (this returns a negative number when traveling backwards)
    private float ForwardSpeed()
    {
        return Vector3.Dot(transform.forward, rigidBody.velocity);
    }

    // Calculate how close the car is to top speed as a number from zero to one
    private float SpeedFactor()
    {
        return Mathf.InverseLerp(0, maxSpeed, ForwardSpeed());
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
    #endregion
}