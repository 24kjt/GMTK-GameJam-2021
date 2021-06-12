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
    public waypointManager wp;                  //Waypoint manager
    public GameObject dancerPrefab;             //Dancer prefab
    
    private float _lastWayPointDistance;        //Last distance that waypoint was spawned at
    private int _congaLength;                   //Lenght of conga line (#dancers)
    public Transform _lastSpawnedWaypoint;     //Last spawned waypoint
    private Vector3 _lastPosition;              //Last position of head of conga line
    private Vector2 _movement;                  //Movement vector for head of conga line
    private Rigidbody2D _rb;                    //Rigid body for head of conga line

    void Start()
    {
        _lastPosition = transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();

        _lastSpawnedWaypoint = wp.yieldWaypoint();
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

        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _rb.velocity = _movement * speed * Time.fixedDeltaTime;
    }

    public void spawnWaypoint(){
        Transform waypoint = wp.yieldWaypoint();

        waypoint.GetComponent<waypointController>().prevWaypoint = _lastSpawnedWaypoint;
        _lastSpawnedWaypoint.GetComponent<waypointController>().nextWaypoint = waypoint;
        _lastSpawnedWaypoint = waypoint;

        _lastWayPointDistance = distanceTraveled;
    }

    public void addDancer(){
        Transform newDancer = Instantiate(dancerPrefab, this.transform.position, this.transform.rotation, this.transform).GetComponent<Transform>(); 
        newDancer.transform.SetParent(this.transform);
        dancers.Add(newDancer);

        _congaLength++;
    }
}
