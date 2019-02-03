using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{

    [SerializeField]
    private GameObject handMarker;
    [SerializeField]
    private Text zAxisText, xAxisText;
    [SerializeField]
    private GameObject notification;

    private float zAxisValue, xAxisValue;

    public int horizontalAxis, verticalAxis;

    public bool isStarted;

    public static InputController instance;

    private void Awake()
    {
        // Ensures only one instance of class can exist
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        xAxisValue = 0;
        zAxisValue = 0;
        isStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        zAxisValue = handMarker.transform.localPosition.z - 3.5f;
        xAxisValue = handMarker.transform.localPosition.x;

        zAxisText.text = "Z: " + zAxisValue.ToString("0.00");
        xAxisText.text = "X: " + xAxisValue.ToString("0.00");

        if (zAxisValue > 0.5)
        {
            verticalAxis = 1;
        }
        else if (zAxisValue < -0.5)
        {
            verticalAxis = -1;
        }
        else
        {
            verticalAxis = 0;
        }

        if (xAxisValue > 0.5)
        {
            horizontalAxis = 1;
        }
        else if (xAxisValue < -0.5)
        {
            horizontalAxis = -1;
        }
        else
        {
            horizontalAxis = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RootPosition")
        {
            Debug.Log("In Null Zone");
            notification.SetActive(true);
            isStarted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RootPosition")
        {
            notification.SetActive(false);
        }
    }
}
