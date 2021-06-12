using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class donkeyKongaController : MonoBehaviour
{
    public float distanceTraveled = 0f;         //Cum. Dist. Travelled
    public int speed = 100;                     //Speed of conga line
    public float waypointFreq = 1f;             //How often to spawn waypoints
    public int startSize = 1;                   //Start size of conga line
    public GameObject waypointPrefab;           //Prefab of waypoint to spawn
    
    private float _lastWayPointDistance;        //Last distance that waypoint was spawned at
    private int _congaLength;                   //Lenght of conga line (#dancers)
    private int _numWayPoints = 0;              //Total number of waypoints
    private GameObject _lastSpawnedWaypoint;    //Last spawned waypoint
    private GameObject _lastWaypoint;           //Last waypoint in list (no prev)
    private Vector3 _lastPosition;              //Last position of head of conga line
    private Vector2 _movement;                  //Movement vector for head of conga line
    private Rigidbody2D _rb;                    //Rigid body for head of conga line

    void Start()
    {
        _lastPosition = transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();

        _lastSpawnedWaypoint = _lastWaypoint = Instantiate(waypointPrefab, transform.position, transform.rotation);
        _lastWayPointDistance = distanceTraveled;

        _congaLength = startSize;
        _numWayPoints++;
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += Vector3.Distance(transform.position, _lastPosition);
        _lastPosition = transform.position;
        Debug.Log(distanceTraveled);

        if (distanceTraveled - _lastWayPointDistance > waypointFreq) {
            spawnWaypoint();
        }

        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _rb.velocity = _movement * speed * Time.fixedDeltaTime;
    }

    public void spawnWaypoint(){

        GameObject waypoint;

        Debug.Log("WAYPOINTS: " + _numWayPoints + " CONGA: " + _congaLength);

        //spawn waypoint if conga is longer than num waypoints
        if (_numWayPoints < _congaLength) {
            waypoint = Instantiate(waypointPrefab, transform.position, transform.rotation);
             waypoint.GetComponent<waypointController>().prevWaypoint = _lastSpawnedWaypoint;
             _lastSpawnedWaypoint.GetComponent<waypointController>().nextWaypoint = waypoint;
             _lastSpawnedWaypoint = waypoint;

            _numWayPoints++;
        } 
        //otherwise move last waypoint to front.
        else {
            //special case for one waypoint
            if (_numWayPoints == 1) {
                _lastWaypoint = _lastSpawnedWaypoint;
                _lastWaypoint.transform.position = transform.position;
            }
            //otherwise move waypoint to front
            else {
                waypoint = _lastWaypoint;
                _lastWaypoint =  _lastWaypoint.GetComponent<waypointController>().nextWaypoint;
                
                //Set prev and next for frontmost waypoint
                waypoint.GetComponent<waypointController>().prevWaypoint = _lastSpawnedWaypoint;
                waypoint.GetComponent<waypointController>().nextWaypoint =  null;

                //Set pointers for old first and new last waypoint
                _lastWaypoint.GetComponent<waypointController>().prevWaypoint = null;
                _lastSpawnedWaypoint.GetComponent<waypointController>().nextWaypoint = waypoint;

                _lastSpawnedWaypoint = waypoint;

                waypoint.transform.position = transform.position; //Move waypoint to new position
            }
         }

           _lastWayPointDistance = distanceTraveled;
    }
}
