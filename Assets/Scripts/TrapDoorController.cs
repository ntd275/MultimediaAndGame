using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorController : MonoBehaviour
{
    public bool IsOpen = true;
    private bool lastState = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            if(lastState != IsOpen)
            {
                lastState = IsOpen;
            if (IsOpen)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
            }
            }
        
    }
}
