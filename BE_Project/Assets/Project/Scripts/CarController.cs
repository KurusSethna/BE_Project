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

    public GameObject vehicle;

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
        Debug.Log("Horizontal: "+horizontalInput);
    }

    private void Accelerate()
    {
        
        velocity = velocity + (verticalInput * motorForce * Time.deltaTime);

        velocity = Mathf.Clamp(velocity, 0, maxVelocity);

        position = vehicle.transform.position;

        vehicle.transform.position = new Vector3(position.x , position.y, position.z + (velocity * Time.deltaTime));
    }

    void Update()
    {
        GetInput();
        if (isStarted)
        {
            //Steer();
            Accelerate();
        }
    }

}


