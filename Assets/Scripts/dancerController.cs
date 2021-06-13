using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dancerController : MonoBehaviour
{
    public bool isLast = true;          //Is this the last dancer in conga line?
    public Transform waypoint = null;   //Waypoint that is being followed
    
    private donkeyKongaController _dk;
    private waypointManager _wp;          //Waypoint Manager
    private int _dancerIndex;

    // Start is called before the first frame update
    void Start()
    {
        _dk = GameObject.Find("DonkeyKonga").GetComponent<donkeyKongaController>();
        _wp = GameObject.Find("WaypointSpawner").GetComponent<waypointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoint)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, waypoint.transform.position, _dk.speed*Time.deltaTime);

            if (transform.position == waypoint.transform.position) {
                //If there is a next waypoint, path to it.
                if(waypoint.GetComponent<waypointController>().nextWaypoint && !waypoint.GetComponent<waypointController>().nextWaypoint.GetComponent<waypointController>().isTargeted ) {
                    Debug.Log("SETTING NEXT WAYPOINT: " + waypoint);
                    waypoint = waypoint.GetComponent<waypointController>().nextWaypoint;
                    Debug.Log("NEW WAYPOINT: " + waypoint);
                    waypoint.GetComponent<waypointController>().isTargeted = true;
                    waypoint.GetComponent<waypointController>().prevWaypoint.GetComponent<waypointController>().isTargeted = false;
                    _dk._lastWaypoint = waypoint;
                    //Delete waypoint if there is a next one and is last
                    if (isLast) {
                        Debug.Log(waypoint.GetComponent<waypointController>().prevWaypoint);
                        Debug.Log(waypoint.GetComponent<waypointController>().isTargeted);
                        _wp.deleteWaypoint(waypoint.GetComponent<waypointController>().prevWaypoint);
                    } 
                }
            }
        }
    }

    public void setDancerIndex(int dancerIndex) {
        this._dancerIndex = dancerIndex;
    }
}
