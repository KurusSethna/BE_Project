using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* AIWaypointNetwork script
 * Use 1: Contains a list of Waypoints. Each Waypoint is a referance to a transform
 * Use 2: Also contains some settings to customize the inspector
 * */

// Display Mode that the Custom Inspector of an AIWaypointNetwork component can be in
public enum PathDisplayMode { None, Connections, Paths }


public class AIWaypointNetwork : MonoBehaviour
{
    [HideInInspector]
    public PathDisplayMode DisplayMode = PathDisplayMode.Connections;   //Current Display Mode
    [HideInInspector]
    public int UIStart = 0, UIEnd = 0;  //Start and End waypoint index for Paths mode

    public List<Transform> Waypoints = new List<Transform>();
}
