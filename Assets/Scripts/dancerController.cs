using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dancerController : MonoBehaviour
{
    public Transform nextDancer;        //Dancer in front
    public Transform prevDancer;        //Dancer behind
    public bool isLast = true;          //Is this the last dancer in conga line?
    public Transform waypoint = null;   //Waypoint that is being followed
    public waypointManager wp;          //Waypoint Manager

    private donkeyKongaController _dk;

    // Start is called before the first frame update
    void Start()
    {
        _dk = GameObject.Find("DonkeyKonga").GetComponent<donkeyKongaController>();
    }

    // Update is called once per frame
    void Update()
    {
        //If no waypoint
        if (!waypoint){
            waypoint = findWaypointToFollow();
        } else {
            isLast = !waypoint.GetComponent<waypointController>().prevWaypoint;

            transform.position = Vector2.MoveTowards(this.transform.position, waypoint.transform.position, _dk.speed*Time.deltaTime);

            if (transform.position == waypoint.position) {
                //If there is a next waypoint, path to it.
                if(waypoint.GetComponent<waypointController>().nextWaypoint) {
                    waypoint = waypoint.GetComponent<waypointController>().nextWaypoint;
                    //Delete waypoint if there is a next one and is last
                    if (isLast) {
                        wp.deleteWaypoint(waypoint.GetComponent<waypointController>().prevWaypoint);
                    }
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
}
