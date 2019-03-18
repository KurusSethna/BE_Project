using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    private bool isStarted;

    [SerializeField]
    private float velocity = 0;
    [SerializeField]
    private float maxVelocity = 0;
    [SerializeField]
    private float motorForce = 0;
    [SerializeField]
    private float acceleration = 0;
    [SerializeField]
    private float turning = 0;
    [SerializeField]
    private float maxTurning = 0;

    [SerializeField]
    private GameObject vehicle;
    [SerializeField]
    private Rigidbody vehicleBody;

    private Vector3 position;
    

    private void Start()
    {
        horizontalInput = 0;
        verticalInput = 0;
        isStarted = false;
    }

    public void GetInput()
    {
        horizontalInput = InputController.instance.horizontalAxis;
        verticalInput = InputController.instance.verticalAxis;
        isStarted = InputController.instance.isStarted;
    }

    private void Steer()
    {
        turning = horizontalInput * maxTurning;
        turning = Mathf.Clamp(turning, -maxTurning, maxTurning);

        vehicle.transform.Rotate(Vector3.up, maxTurning/turning );
    }

    private void Accelerate()
    {
        acceleration = (motorForce * verticalInput) / 1050 ;
        
        velocity = velocity + ( acceleration );

        velocity = Mathf.Clamp(velocity, 0, maxVelocity);

        vehicleBody.AddForce(0, 0, motorForce);
    }

    void FixedUpdate()
    {
        GetInput();
        if (isStarted)
        {
            //Steer();
            Accelerate();
        }
    }

}


