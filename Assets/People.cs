using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{
    public bool joinedLine = false; //are you in line?

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PolygonEncloser>() )
        {
            Debug.Log("people captured");
        }
        else
        {
            Debug.Log("Entered but not captured");
        }
    }
}
