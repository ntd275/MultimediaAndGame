using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLv4 : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isOpen;
    public GameObject TrapDoor;
    void Start()
    {
        isOpen = GetComponent<SwitchController>().IsOn;
        TrapDoor.GetComponent<TrapDoorController>().IsOpen = isOpen;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<SwitchController>().IsOn != isOpen)
        {
            isOpen = !isOpen;
            TrapDoor.GetComponent<TrapDoorController>().IsOpen = isOpen;
        }
    }
}
