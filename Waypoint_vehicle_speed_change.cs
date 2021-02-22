using UnityEngine;
using System.Collections;

public class WaypointManager : MonoBehaviour
{
    public GameObject WayPointHolder;
    private Transform[] waypoints;
    public MyOwnCarTest CarObject;
    private int x;// = 1;
                  // Use this for initialization
    void Start()
    {
        waypoints = WayPointHolder.GetComponentsInChildren<Transform>();
        //Daeyeol Chang	Debug.Log (waypoints.Length + "Objects found");
        x = 1;
        CarObject.locationObject = waypoints[x];
        foreach (Transform y in waypoints)
        {
            //Daeyeol Chang		Debug.Log (y.position.x +", " +  y.position.y+", " + y.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            x++;
            CarObject.locationObject = waypoints[x];
        }

    }
    void OnTriggerEnter(Collider trigger)
    {

        if (trigger.name == "Waypoint")
        {
            if (x == waypoints.Length - 1)
            {
                x = 2;
                CarObject.gameObject.transform.position = new Vector3(waypoints[0].position.x, waypoints[0].position.y + 2, waypoints[0].position.z);
                CarObject.gameObject.transform.rotation = Quaternion.Euler(waypoints[0].rotation.x, waypoints[0].rotation.y - 90, waypoints[0].rotation.z);
                CarObject.locationObject = waypoints[x];

            }
            else
            {

                x++;
                //	Debug.Log ("Hit a waypoint " + waypoints[x].position.ToString ());
                CarObject.locationObject = waypoints[x];
            }


        }

        else if (trigger.name == "Waypoint_slowTo50")
        {
            CarObject.ChangeTorque(8500.0f);
            CarObject.ApplyBrakeTorque(1500.0f);
        }
        else if (trigger.name == "Waypoint_speedTo70")
        {
            CarObject.ChangeTorque(8000.0f);
            CarObject.ApplyBrakeTorque(500.0f);

        }
        else if (trigger.name == "Waypoint_slowTo34")
        {
            CarObject.ChangeTorque(6500.0f);
            CarObject.ApplyBrakeTorque(2000.0f);
        }
        else if (trigger.name == "Waypoint_slowTo33")
        {
            CarObject.ChangeTorque(7500.0f);
            CarObject.ApplyBrakeTorque(2500.0f);

        }
        else if (trigger.name == "Waypoint_speedTo60")
        {
            CarObject.ChangeTorque(15000.0f);
            CarObject.ApplyBrakeTorque(0.0f);
        }

        else if (trigger.name == "Waypoint_speedTo51")
        {
            CarObject.ChangeTorque(8000.0f);
            CarObject.ApplyBrakeTorque(1000.0f);
        }
        else if (trigger.name == "Waypoint_speedTo50")
        {
            //         CarObject.ChangeTorque(8000.0f);
            //         CarObject.ApplyBrakeTorque(1000.0f);
            CarObject.ChangeTorque(8000.0f);
            CarObject.ApplyBrakeTorque(1500.0f);

        }
    }
}