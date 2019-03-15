using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/* Navigation Controller script
 * This script handles the AI navigation of objects in the scene along a predifined path
 * Attach this script to all objects that must follow movement along a predifined path
 * */

[RequireComponent(typeof(NavMeshAgent))]    // Ensures that script can only be attached to objects that also include a NavMeshAgent component
public class NavigationController : MonoBehaviour
{

    [SerializeField]
    AIWaypointNetwork WaypointNetwork;
    [SerializeField]
    NavMeshPathStatus PathStatus = NavMeshPathStatus.PathInvalid;

    [SerializeField]
    int currentIndex = 0;
    [SerializeField]
    bool hasPath = false, pathPending = false, pathStale = false;

    NavMeshAgent navAgent = null;


    // Start is called before the first frame update
    void Start()
    {

        navAgent = GetComponent<NavMeshAgent>();    // Cache NavMeshAgent Reference

        if (WaypointNetwork == null)
        {
            return;     // If no valid Waypoint Network has been assigned, then return
        }

        SetNextDestination(false);      // Call function

    }

    // Optionally increments the current waypoint index and sets the next destination for the agent to head towards
    void SetNextDestination(bool increment)
    {

        if (!WaypointNetwork)
        {
            return;     // Return if there is no waypoint network
        }

        int incStep = increment ? 1 : 0;    // Calculate whether to increment the current waypoint index

        // Calculate index of next waypoint factoring in the increment, with wraparound
        int nextWaypoint = (currentIndex + incStep) % WaypointNetwork.Waypoints.Count;

        // Fetch waypoint
        Transform nextWaypointTransform = WaypointNetwork.Waypoints[nextWaypoint];

        if (nextWaypointTransform != null)      // Assuming valid waypoint transform
        {
            // Update the current waypoint index, assign its position as the NavMeshAgent's destination and then return
            currentIndex = nextWaypoint;
            navAgent.destination = nextWaypointTransform.position;
            return;
        }

        // Increment current index if valid waypoint is not found in the list during this iteration
        currentIndex++;

    }

    // Update is called once per frame
    void Update()
    {

        // Copy NavMeshAgent state variables into inspector visible variables
        hasPath = navAgent.hasPath;
        pathPending = navAgent.pathPending;
        pathStale = navAgent.isPathStale;
        PathStatus = navAgent.pathStatus;

        // If there is no current path and one is not pending then set the next waypoint as the target
        // Else if path is stale, regenerate path
        if ((!hasPath && !pathPending) || PathStatus == NavMeshPathStatus.PathInvalid)
        {
            SetNextDestination(true);
        }
        else if (navAgent.isPathStale)
        {
            SetNextDestination(false);
        }

    }
}
