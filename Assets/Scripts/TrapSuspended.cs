using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSuspended : MonoBehaviour
{
    // Start is called before the first frame update
    public float LimitAngle = 60;
    public Vector3 rotateVector = new Vector3(0, 0, 1);
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotateVector);
        var curAngle = transform.rotation.eulerAngles.z;
        //Debug.Log(transform.rotation.eulerAngles.z);
        if(curAngle >= LimitAngle && curAngle <= 360-LimitAngle)
        {
            rotateVector *= -1;
        }
    }
}
