using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointManager : MonoBehaviour
{
    //transform.hierarchyCount
    public GameObject waypointPrefab;           //Prefab of waypoint to spawn

    public Transform yieldWaypoint() {
        Transform waypoint;
        
        waypoint = transform.GetChild(0);
        waypoint.gameObject.SetActive(true);
        waypoint.SetParent(null);

        //set waypoint head is carrying to active
        if (this.transform.childCount > 0) {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        //Spawn waypoint if none left (for next time)
        else {
            Instantiate(waypointPrefab, transform.position, transform.rotation).GetComponent<Transform>().SetParent(this.transform);
        }

        return waypoint;
    }

    //Assume waypoint is always at the end
    public void deleteWaypoint(Transform waypoint){
        waypointController wc = waypoint.GetComponent<waypointController>();

        //Remove connection from future waypoint if it exists
        if (wc.nextWaypoint) {
            wc.nextWaypoint.GetComponent<waypointController>().prevWaypoint = null;
        }

        wc.nextWaypoint = wc.prevWaypoint = null;
        waypoint.SetParent(this.transform);
        wc.transform.position =this.transform.position;
        waypoint.gameObject.SetActive(false);
    }
}
