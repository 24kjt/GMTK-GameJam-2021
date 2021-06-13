using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dancerController : MonoBehaviour
{
    public bool isLast = true;          //Is this the last dancer in conga line?
    public Transform waypoint = null;   //Waypoint that is being followed
    
    private donkeyKongaController _dk;
    private waypointManager _wp;          //Waypoint Manager
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private int _dancerIndex;
    private bool _atWaypoint = false;

    // Start is called before the first frame update
    void Start()
    {
        _dk = GameObject.Find("DonkeyKonga").GetComponent<donkeyKongaController>();
        _wp = GameObject.Find("WaypointSpawner").GetComponent<waypointManager>();
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoint)
        {
            // transform.position = Vector2.MoveTowards(this.transform.position, waypoint.transform.position, _dk.speed*Time.deltaTime);
            if (!_atWaypoint) {
                _direction = (waypoint.transform.position - this.transform.position).normalized;
                _rb.velocity = new Vector2(_direction.x * _dk.speed * Time.fixedDeltaTime, _direction.y * _dk.speed * Time.fixedDeltaTime);
            } else {
                _rb.velocity = Vector2.zero;
            }

            if (Mathf.Abs(transform.position.magnitude - waypoint.transform.position.magnitude) < _dk.waypointMarginOfError) {
                //If there is a next waypoint, path to it.
                if(waypoint.GetComponent<waypointController>().nextWaypoint && !waypoint.GetComponent<waypointController>().nextWaypoint.GetComponent<waypointController>().isTargeted ) {
                    waypoint = waypoint.GetComponent<waypointController>().nextWaypoint;
                    waypoint.GetComponent<waypointController>().isTargeted = true;
                    waypoint.GetComponent<waypointController>().prevWaypoint.GetComponent<waypointController>().isTargeted = false;
                    _dk._lastWaypoint = waypoint;
                    //Delete waypoint if there is a next one and is last
                    if (isLast) {
                        _wp.deleteWaypoint(waypoint.GetComponent<waypointController>().prevWaypoint);
                    } 
                    _atWaypoint = false;
                } else {
                    _atWaypoint = true;
                }
            }
        }
    }

    public void setDancerIndex(int dancerIndex) {
        this._dancerIndex = dancerIndex;
    }
}
