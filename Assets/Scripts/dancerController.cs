using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dancerController : MonoBehaviour
{
    public bool isLast = false;          //Is this the last dancer in conga line?
    public Transform waypoint = null;   //Waypoint that is being followed
    public waypointManager wp;          //Waypoint Manager

    private donkeyKongaController _dk;
    private int _dancerIndex;

    // Start is called before the first frame update
    void Start()
    {
        _dk = GameObject.Find("DonkeyKonga").GetComponent<donkeyKongaController>();
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
                    waypoint = waypoint.GetComponent<waypointController>().nextWaypoint;
                    waypoint.GetComponent<waypointController>().isTargeted = true;
                    //Delete waypoint if there is a next one and is last
                    if (isLast) {
                        // wp.deleteWaypoint(waypoint.GetComponent<waypointController>().prevWaypoint);
                    } else {
                        //otherwise flag new waypoint as available for targeting
                       
                    }
                     waypoint.GetComponent<waypointController>().prevWaypoint.GetComponent<waypointController>().isTargeted = false;
                }
            }
        }
    }

     public Transform findWaypointToFollow(){
        Transform cur = _dk._lastSpawnedWaypoint;
        bool waypointFound = false;

        while(!waypointFound) {
            //If no one is following the waypoint pick this one
            if (!cur.GetComponent<waypointController>().isTargeted){
                waypointFound = true;
            } else {
                if (cur.GetComponent<waypointController>().prevWaypoint) {
                    cur = cur.GetComponent<waypointController>().prevWaypoint;
                } else {
                    //default to end of conga line if can't find waypoint to use.
                    cur = null;
                    waypointFound = true;
                }
            }
        }

        //indicate that waypoint is now targeted
        if (cur) {
            cur.GetComponent<waypointController>().isTargeted = true;
        }

        return cur; 
    }

    public void setDancerIndex(int dancerIndex) {
        this._dancerIndex = dancerIndex;
    }
}
