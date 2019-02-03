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
        
    }

    // Update is called once per frame
    void Update()
    {
        zAxisValue = handMarker.transform.localPosition.z;
        xAxisValue = handMarker.transform.localPosition.x;

        zAxisText.text = "Z: " + zAxisValue.ToString("0.00");
        xAxisText.text = "X: " + xAxisValue.ToString("0.00");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RootPosition")
        {
            Debug.Log("In Null Zone");
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
