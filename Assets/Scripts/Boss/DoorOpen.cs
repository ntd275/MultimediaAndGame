using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject Door;
    private BossCombat bc;
    private bool isOpen = false;
    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BossCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bc.Health <= 0 && !isOpen)
        {
            isOpen = true;
            StartCoroutine(FollowDoor(2));
        }
    }

    IEnumerator FollowDoor(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Camera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = Door.transform;
        Door.GetComponent<Animator>().SetBool("Open", true);
        Door.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(FollowPlayer(2));
    }

    IEnumerator FollowPlayer(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Camera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = GameObject.Find("Player").transform;
    }
}
