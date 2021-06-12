using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class congaController : MonoBehaviour
{
    public List<Transform>dancers = new List<Transform>();
    public float minDistance = 0.25f;
    public int startSize = 1;
    public float speed = 1f;
    public float rotationSpeed = 50;
    public GameObject dancerPrefab;

    private Transform _curDancer;
    private Transform _prevDancer;
    private float _dis;
    private Vector2 _movement;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize conga line
        for (int i = 0; i < startSize - 1; i++) {
            addDancer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //Debug add dancer
        if (Input.GetKeyDown(KeyCode.Q))
            addDancer();
    }

    public void addDancer(){
        Debug.Log(dancers.Count);
        Transform newDancer = (Instantiate(dancerPrefab, dancers[dancers.Count - 1].position, dancers[dancers.Count - 1].rotation) as GameObject).transform;
        newDancer.SetParent(transform);
        dancers.Add(newDancer);
    }

    public void Move(){
        float curspeed = speed;
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        dancers[0].GetComponent<Rigidbody2D>().MovePosition(dancers[0].GetComponent<Rigidbody2D>().position + _movement * speed * Time.deltaTime);
        Debug.Log("Head Pos: " + dancers[0].GetComponent<Rigidbody2D>().position);
        for (int i = 1; i < dancers.Count; i++) {
            _curDancer = dancers[i];
            _prevDancer = dancers[i-1];

            Vector3 curpos = _curDancer.position;
            Vector3 moveDirection = (_prevDancer.position - curpos);
        
        if (_movement.magnitude != 0) {

            float distanceToTarget = moveDirection.magnitude;
            if ( distanceToTarget > speed)
                distanceToTarget = speed;

            moveDirection.Normalize();
            Vector3 target = moveDirection * distanceToTarget * minDistance + curpos;
            _curDancer.position = Vector3.Lerp(curpos, target, speed * Time.deltaTime);
        }
            // _dis = Vector3.Distance(_prevDancer.position, _curDancer.position);

            // Vector3 newpos = _prevDancer.position;

            // float T = Time.deltaTime*_dis/minDistance*curspeed;

            // //stop lag
            // if (T>0.5f)
            //     T = 0.5f;

            // _curDancer.position = Vector3.Slerp(_curDancer.position, newpos, T);

        }
    }
}
