using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class donkeyKongaController : MonoBehaviour
{
    public float distanceTraveled = 0f;
    public int speed = 100;
    public float waypointFreq = 1f;
    public GameObject waypointPrefab;
    
    private float _lastWayPointDistance;
    private GameObject _lastSpawnedWaypoint;
    private Vector3 _lastPosition;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    void Start()
    {
        _lastPosition = transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();

        _lastSpawnedWaypoint = Instantiate(waypointPrefab, transform.position, transform.rotation);
        _lastWayPointDistance = distanceTraveled;
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
        GameObject waypoint = Instantiate(waypointPrefab, transform.position, transform.rotation);
        waypoint.GetComponent<waypointController>().prevWaypoint = _lastSpawnedWaypoint;
        _lastSpawnedWaypoint.GetComponent<waypointController>().nextWaypoint = waypoint;
        _lastSpawnedWaypoint = waypoint;
        _lastWayPointDistance = distanceTraveled;
    }
}
