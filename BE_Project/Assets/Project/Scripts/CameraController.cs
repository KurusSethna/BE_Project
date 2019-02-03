using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Main Camera Controller Script
 * Script is a singleton so only one instance will exist
 * Use 1: Sets MainCamera ClearFlag to skybox
 * Use 2: Sets position of ARCamera to below MainCamera to avoid intersect
 * Use 3: Rotates ARCamera along with MainCamera
 * Use 4: Moves HandMarker to track position of HandTracker
 * */


public class CameraController : MonoBehaviour
{

    [SerializeField]
    private GameObject handMarker;
    [SerializeField]
    private GameObject handTracker;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Camera ARCamera;

    //Create static instance of class
    public static CameraController instance;

    private Vector3 correction = new Vector3(0f, 10f, 0f);
    private Vector3 position = new Vector3(0.0f, -10f, 0.0f);

    // Awake is called when the script instance is being loaded.  
    // Awake is called only once during the lifetime of the script instance. 
    // Awake is called after all objects are initialized
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
        // Sets initial state of cameras
        camera.clearFlags = CameraClearFlags.Skybox;
        ARCamera.transform.position += position;
    }

    // Update is called once per frame
    void Update()
    {
        // Adjusts orientation of camera
        ARCamera.transform.rotation = camera.transform.rotation;
    }

    // LateUpdate is called after all update functions have been called
    private void LateUpdate()
    {
        // Moves marker to track handtracker
        handMarker.transform.position = handTracker.transform.position + correction;
    }
}
