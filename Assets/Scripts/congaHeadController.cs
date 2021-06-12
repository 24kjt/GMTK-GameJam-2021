using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class congaHeadController : MonoBehaviour
{
    public List<Transform>dancers = new List<Transform>();
    public GameObject dancerPrefab;
    public int speed = 20;
    public int turnSpeed = 20;
    public int startSize = 1;
    private Vector3 _mouse;
    private Vector2 _direction;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Initialize conga line
        for (int i = 1; i < startSize - 1; i++) {
            addDancer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = (_mouse - transform.position).normalized;
        rb.velocity = new Vector2(_direction.x * speed * Time.deltaTime, _direction.y * speed * Time.deltaTime);
    }

    public void addDancer(){
        Transform newDancer = (Instantiate(dancerPrefab, dancers[dancers.Count - 1].position, dancers[dancers.Count - 1].rotation, transform) as GameObject).transform;
        Debug.Log(dancers.Count);
        if (dancers.Count == 0)
             newDancer.GetComponent<congaDancerController>().leader = transform;
        else
            newDancer.GetComponent<congaDancerController>().leader = dancers[dancers.Count - 1];
        dancers.Add(newDancer);
    }
}
