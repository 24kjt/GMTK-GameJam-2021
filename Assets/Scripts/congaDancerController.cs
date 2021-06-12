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
        _speed = 30;
        _rb = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (leader) {
            setTarget();
             _rb.velocity = new Vector2(target.x * _speed * Time.deltaTime, target.y * _speed * Time.deltaTime);
        }
    }

    IEnumerator setTarget() {
        yield return new WaitForSeconds(0.2f);

        target = leader.position;
    }
}
