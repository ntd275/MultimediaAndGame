using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginGameTitle : MonoBehaviour
{
    public float Duration = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
