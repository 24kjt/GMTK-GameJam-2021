using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class congaDancerController : MonoBehaviour
{
    public Transform leader;
    public Vector2 target;
    private int _speed;
    private Rigidbody2D _rb;
    private congaHeadController _parent; 

    // Start is called before the first frame update
    void Start()
    {
        // _parent = gameObject.GetComponentInParent<congaHeadController>();
        Debug.Log(_parent);
        _speed = 1000;
        _rb = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (leader) {
            Debug.Log("We got here");
            StartCoroutine(setTarget());
        }
    }

    IEnumerator setTarget() {
        target.x = leader.position.x - transform.position.x;
        target.y = leader.position.y - transform.position.y;
        target.Normalize();
        _rb.velocity = new Vector2(target.x * _speed * Time.deltaTime, target.y * _speed * Time.deltaTime);
        Debug.Log("SET VELOCTY");
        yield return new WaitForSeconds(0.2f);
    }
}