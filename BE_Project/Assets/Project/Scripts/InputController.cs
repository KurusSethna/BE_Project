using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Input Controller script
 * Use 1- Gets position of handmarker and converts it into input
 * Use 2- Begins simulation when marker enters null position 
 * */

public class InputController : MonoBehaviour
{

    // Serialized Game Objects

    [SerializeField]
    private GameObject handMarker;
    [SerializeField]
    private Text zAxisText, xAxisText;
    [SerializeField]
    private GameObject notification;

    // Private variables

    private float zAxisValue, xAxisValue;

    // Public variables

    public float horizontalAxis, verticalAxis;

    public bool isStarted;

    // Creating static instance of class

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
        // Initialize Values
        xAxisValue = 0;
        zAxisValue = 0;
        isStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get position of hand marker local to camera
        zAxisValue = handMarker.transform.localPosition.z - 3.5f;
        xAxisValue = handMarker.transform.localPosition.x;

        // Display position of hand marker
        zAxisText.text = "Z: " + zAxisValue.ToString("0.00");
        xAxisText.text = "X: " + xAxisValue.ToString("0.00");

        // Obtain input values for horizontal and vertical axis after normalising hand marker position values
        verticalAxis = Mathf.Clamp(zAxisValue, -3.5f, 3.5f) / 3.5f;
        horizontalAxis = Mathf.Clamp(xAxisValue, -3.5f, 3.5f) / 3.5f;

    }

    // Notifies user when hand marker enters and exits null zone

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Null Point")
        {
            // Begins simulation only when hand marker enters null zone for the first time
            isStarted = true;

            // Notifies user when marker is in null zone
            notification.SetActive(true);
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
