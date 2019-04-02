using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Car Controller script
 * Use 1- Gets input values from Input Controller script
 * Use 2- Handles acceleration and steering according to input 
 * */

public class CarController : MonoBehaviour
{

    // Serialized Game Objects

    [SerializeField]
    private WheelCollider frontRightWheel, frontLeftWheel, rearRightWheel, rearLeftWheel;

    // Private variables

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    private bool isStarted;

    // Public variables- Will vary as per simulated vehicle

    public float maxSteeringAngle;
    public float motorForce;

    private void Start()
    {
        // Initialize variables
        horizontalInput = 0;
        verticalInput = 0;
        isStarted = false;
    }

    // Function to get input values
    public void GetInput()
    {
        // Get input values from InputController
        horizontalInput = InputController.instance.horizontalAxis;
        verticalInput = InputController.instance.verticalAxis;
        isStarted = InputController.instance.isStarted;
    }

    // Function to handle steering
    private void Steer()
    {
        // Calculate steering angle
        steeringAngle = maxSteeringAngle * horizontalInput;

        // Turn wheels as per steering able
        frontLeftWheel.steerAngle = steeringAngle;
        frontRightWheel.steerAngle = steeringAngle;
    }

    // Function to handle acceleration
    private void Accelerate()
    {
        // Calculate motor force wheels
        frontRightWheel.motorTorque = verticalInput * motorForce;
        frontLeftWheel.motorTorque = verticalInput * motorForce;
    }

    // Fixed Update function is called at regular intervals, depending on frame rate of device
    void FixedUpdate()
    {
        GetInput();
        if (isStarted)
        {
            Steer();
            Accelerate();
        }
    }

}


