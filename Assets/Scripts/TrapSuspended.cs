using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSuspended : MonoBehaviour
{
    // Start is called before the first frame update
    public float LimitAngle = 60;
    public float Speed = 3;
    private float rotated = 0;
    private Vector3 rotateVector;
    void Start()
    {
        rotateVector = new Vector3(0, 0, Speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotateVector);
        rotated += rotateVector.z;
        if(Mathf.Abs(rotated) >= LimitAngle)
        {
            rotateVector *= -1;
        }
    }
}
