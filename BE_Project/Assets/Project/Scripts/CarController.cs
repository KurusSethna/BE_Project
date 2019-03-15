using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    private bool isStarted;

    public WheelCollider frontRightWheel, frontLeftWheel, rearRightWheel, rearLeftWheel;

    public float maxSteeringAngle = 30.0f;
    public float motorForce = 50.0f;

    private void Start()
    {
        horizontalInput = 0;
        verticalInput = 0;
        isStarted = false;
    }

    public void GetInput()
    {
        horizontalInput = InputController.instance.horizontalAxis;
        //horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = InputController.instance.verticalAxis;
        //verticalInput = Input.GetAxis("Vertical");
        isStarted = InputController.instance.isStarted;
    }

    private void Steer()
    {
        steeringAngle = maxSteeringAngle * horizontalInput;

        Debug.Log("Horizontal: "+horizontalInput);
        frontLeftWheel.steerAngle = steeringAngle;
        frontRightWheel.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        frontRightWheel.motorTorque = verticalInput * motorForce;
        Debug.Log("Vertical: "+verticalInput);
        frontLeftWheel.motorTorque = verticalInput * motorForce;
    }

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


