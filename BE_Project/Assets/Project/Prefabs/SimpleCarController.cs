using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;

    public WheelCollider frontRightWheel, frontLeftWheel, rearFrontWheel, rearLeftWheel;
    //public Transform frontRightWheelTransform, frontLeftWheelTransform, rearFrontWheelTransform, rearLeftWheelTransform;
    public float maxSteeringAngle = 30.0f;
    public float motorForce = 50.0f;

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steeringAngle = maxSteeringAngle * horizontalInput;

        frontLeftWheel.steerAngle = steeringAngle;
        frontRightWheel.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        frontRightWheel.motorTorque = verticalInput * motorForce;
        frontLeftWheel.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {

    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {

    }

    void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

}
