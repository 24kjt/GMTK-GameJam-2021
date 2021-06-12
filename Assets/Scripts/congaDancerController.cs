using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class congaDancerController : MonoBehaviour
{
    public Transform leader;
    public Vector2 target;
    [SerializeField ]private int _speed;
    private Rigidbody2D _rb;
    private congaHeadController _parent; 

    // Start is called before the first frame update
    void Start()
    {
        // _parent = gameObject.GetComponentInParent<congaHeadController>();
        Debug.Log(_parent);
        _speed = 1000;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        //StartCoroutine(setTarget());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (leader) {
            Debug.Log("We got here");
            //StartCoroutine(setTarget());
        Debug.Log("LEADER: " + leader.position);
        target.x = leader.position.x - transform.position.x;
        target.y = leader.position.y - transform.position.y;
        Debug.Log("Target: " + target);
        target.Normalize();
        Debug.Log(target * _speed * Time.fixedDeltaTime);
        _rb.velocity = new Vector2(target.x * _speed * Time.fixedDeltaTime, target.y * _speed * Time.fixedDeltaTime);
        Debug.Log("SET VELOCTY");
        }
    }

    IEnumerator setTarget() {
        yield return new WaitForSeconds(1f);
        Debug.Log("LEADER: " + leader.position);
        target.x = leader.position.x - transform.position.x;
        target.y = leader.position.y - transform.position.y;
        Debug.Log("Target: " + target);
        target.Normalize();
        Debug.Log(target * _speed * Time.fixedDeltaTime);
        _rb.velocity = new Vector2(target.x * _speed * Time.fixedDeltaTime, target.y * _speed * Time.fixedDeltaTime);
        Debug.Log("SET VELOCTY");
        StartCoroutine(setTarget());
        //yield return new WaitForSeconds(0.2f);
    }
}
