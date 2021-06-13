using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class donkeyKongaController : MonoBehaviour
{
    public List<Transform>dancers = new List<Transform>();      //List of dancers
    public float distanceTraveled = 0f;         //Cum. Dist. Travelled
    public int speed = 100;                     //Speed of conga line
    public float waypointFreq = 1f;             //How often to spawn waypoints
    public int startSize = 1;                   //Start size of conga line
    public float waypointMarginOfError = 0.2f;  //How close counts as being on top of waypoint
    public waypointManager wp;                  //Waypoint manager
    public GameObject dancerPrefab;             //Dancer prefab
    
    private float _lastWayPointDistance;        //Last distance that waypoint was spawned at
    private int _congaLength;                   //Lenght of conga line (#dancers)
    public Transform _lastSpawnedWaypoint;      //Last spawned waypoint
    public Transform _lastWaypoint;            //Last Waypoint
    private Vector3 _lastPosition;              //Last position of head of conga line
    private Vector2 _movement;                  //Movement vector for head of conga line
    private Rigidbody2D _rb;                    //Rigid body for head of conga line
    private Vector3 _mouse;
    private Vector3 _direction;

    void Start()
    {
        _lastPosition = transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();

        _lastSpawnedWaypoint = _lastWaypoint = wp.yieldWaypoint();
        _lastWayPointDistance = distanceTraveled;

        for(int i = 0; i < startSize - 1; i++){
            addDancer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += Vector3.Distance(transform.position, _lastPosition);
        _lastPosition = transform.position;

        if (distanceTraveled - _lastWayPointDistance > waypointFreq) {
            spawnWaypoint();
        }

        // _movement.x = Input.GetAxisRaw("Horizontal");
        // _movement.y = Input.GetAxisRaw("Vertical");
        // _rb.velocity = _movement * speed * Time.fixedDeltaTime;

        _mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = (_mouse - transform.position).normalized;
        _rb.velocity = new Vector2(_direction.x * speed * Time.fixedDeltaTime, _direction.y * speed * Time.fixedDeltaTime);
       

        //Debug add dancer
        if (Input.GetKeyDown(KeyCode.Q))
            addDancer();
    }

    public void spawnWaypoint(){
        Transform waypoint = wp.yieldWaypoint();

        waypoint.GetComponent<waypointController>().prevWaypoint = _lastSpawnedWaypoint;
        _lastSpawnedWaypoint.GetComponent<waypointController>().nextWaypoint = waypoint;
        _lastSpawnedWaypoint = waypoint;

        _lastWayPointDistance = distanceTraveled;
    }

    public void addDancer(){

        Transform waypoint = wp.yieldWaypoint();
        Transform newDancer;

        //Special case for first dancer
        if (dancers.Count == 0) {
            newDancer = Instantiate(dancerPrefab, this.transform.position, this.transform.rotation, this.transform).GetComponent<Transform>();
            waypoint.transform.position = this.transform.position;
        } else {
            newDancer = Instantiate(dancerPrefab, dancers[dancers.Count - 1].transform.position, dancers[dancers.Count - 1].transform.rotation, this.transform).GetComponent<Transform>(); 
            waypoint.transform.position = dancers[dancers.Count - 1].transform.position;

            //last is no longer last :O
            dancers[dancers.Count - 1].GetComponent<dancerController>().isLast = false;
        }
        
        //Pointers for new waypoint
        _lastWaypoint.GetComponent<waypointController>().prevWaypoint = waypoint;
        waypoint.GetComponent<waypointController>().nextWaypoint = _lastWaypoint;
        _lastWaypoint = waypoint;

        //Set waypoint for new dancer
        waypoint.GetComponent<waypointController>().isTargeted = true;
        newDancer.GetComponent<dancerController>().waypoint = waypoint;

        newDancer.transform.SetParent(null);
        dancers.Add(newDancer);
        newDancer.GetComponent<dancerController>().setDancerIndex(dancers.Count - 1);
        _congaLength++;
    }
}
