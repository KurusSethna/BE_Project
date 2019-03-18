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

    public float horizontalAxis, verticalAxis;

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
        zAxisValue = handMarker.transform.localPosition.z - 4.5f;
        xAxisValue = handMarker.transform.localPosition.x;

        zAxisText.text = "Z: " + zAxisValue.ToString("0.00");
        xAxisText.text = "X: " + xAxisValue.ToString("0.00");

        verticalAxis = Mathf.Clamp(zAxisValue, -3.5f, 3.5f) / 3.5f;
        horizontalAxis = Mathf.Clamp(xAxisValue, -3.5f, 3.5f) / 3.5f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RootPosition")
        {
            Debug.Log("In Null Zone");
            notification.SetActive(true);
        }
        if(other.tag=="Zero Point")
        {
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
