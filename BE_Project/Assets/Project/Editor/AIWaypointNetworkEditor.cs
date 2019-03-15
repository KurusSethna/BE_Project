using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

/* AIWaypointNetworkEditor script
* Must be placed in Editor directory
* Use 1: Customizes inspector for AIWaypointNetwork script object
* Use 2: Enables Scene View Rendering for the AIWaypointNetworkComponent
* */

[CustomEditor(typeof(AIWaypointNetwork))]   //This script is a custom editor for AIWaypointNetwork script
public class AIWaypointNetworkEditor : Editor
{
    // OnInspectorGUI override function
    // Called by Unity Editor when the Inspector needs repainting for an AIWaypointNetwork Component
    public override void OnInspectorGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target;  // Reference to selected component

        // Show the Display Mode Enumeration Selector
        network.DisplayMode = (PathDisplayMode)EditorGUILayout.EnumPopup("Display Mode", network.DisplayMode);

        // If in paths display mode, display the integer sliders for the Start and End waypoint indices
        if (network.DisplayMode == PathDisplayMode.Paths)
        {
            network.UIStart = EditorGUILayout.IntSlider("Waypoint Start", network.UIStart, 0, network.Waypoints.Count - 1);
            network.UIEnd = EditorGUILayout.IntSlider("Waypoint End", network.UIEnd, 0, network.Waypoints.Count - 1);
        }

        // Tell Unity to do its default drawing on all serialized members that are NOT hidden in the inspector
        DrawDefaultInspector();
    }

    // OnSceneGUI function
    // This function will be called by the Unity Editor when the Scene View is being repainted. This gives us a hook to do our own rendering to the Scene View
    private void OnSceneGUI()
    {

        AIWaypointNetwork network = (AIWaypointNetwork)target;  // Reference to the component being rendered

        // Fetch all waypoints from the network and render a label for each one
        for(int i = 0; i <= network.Waypoints.Count; i++)
        {
            if (network.Waypoints[i] != null)
            {
                Handles.Label(network.Waypoints[i].position, "Waypoint " + i.ToString());
            }
        }

        // If we are in connections mode then draw lines connection all waypoints
        if (network.DisplayMode == PathDisplayMode.Connections)
        {
            // Allocate array of vector to store the polyline positions
            Vector3[] linePoints = new Vector3[network.Waypoints.Count - 1];

            // Loop through each waypoint + one additional interaction
            for (int i = 0; i <= network.Waypoints.Count; i++)
            {
                // Calculate the waypoint index with wrap around
                int index = i % (network.Waypoints.Count + 1);

                // Fetch the position of the waypoint for this interaction and copy it into the vector array
                if (network.Waypoints[index] != null)
                {
                    linePoints[i] = network.Waypoints[index].position;
                }
                else
                {
                    linePoints[i] = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
                }
            }


         // Set the handle colour to cyan
         Handles.color = Color.cyan;

          // Render the polyline in the scene view by passing in our list of waypoint positions
         Handles.DrawPolyLine(linePoints);

        }
        else if (network.DisplayMode == PathDisplayMode.Paths)  // we are in paths mode so we must show proper navmesh path search and render results
        {

            NavMeshPath path = new NavMeshPath();   // Allocate a new NavMeshPath

            // Assuming both the start and end waypoint indices selected are legit
            if(network.Waypoints[network.UIStart]!=null && network.Waypoints[network.UIEnd] != null)
            {

                // Fetch positions on start and end vectors from the waypoint network
                Vector3 from = network.Waypoints[network.UIStart].position;
                Vector3 to = network.Waypoints[network.UIEnd].position;

                // Request a path to search on NavMesh
                // This will return the path between from and to vectors
                NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);

                // Set Handles colour to Yellow
                Handles.color = Color.yellow;

                // Draw a polyline passing through the path's corner points
                Handles.DrawPolyLine(path.corners);

            }

        }
        

    }

}
