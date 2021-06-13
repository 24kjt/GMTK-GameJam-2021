using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosureCapturer : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> linePointObjects;
    public List<Vector2> linePoints;
    public PolygonCollider2D enclosedSpace;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetPointsFromObjects(); 
            EncloseSpace(linePoints);
        }
    }

    void GetPointsFromObjects() //get vertex positions for polygon from objects
    {
        linePoints.Clear();

        foreach(Transform points in linePointObjects)
        {
            linePoints.Add(points.position);
        }
    }

    void EncloseSpace(List<Vector2> points) //create polyon collider shape
    {
        enclosedSpace.offset = -points[0];
        enclosedSpace.SetPath(0, points);
    }

    void OnTriggerEnter2D(Collider2D other) //check if other collider is a person in line. Get its placing
    {
        
    }
}
