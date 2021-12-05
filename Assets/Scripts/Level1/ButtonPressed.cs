using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    public GameObject Door;
    public GameObject Camera;
    public float TimeChangeFollow = 2;
    private HashSet<GameObject> colliderList = new HashSet<GameObject>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colliderList.Count == 0)
        {
            Door.GetComponent<Animator>().SetBool("Open", true);
            Door.GetComponent<BoxCollider2D>().enabled = false;
            Camera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = Door.transform;
            StopCoroutine("FollowPlayer");
            StartCoroutine(FollowPlayer(TimeChangeFollow));
        }
        colliderList.Add(collision.gameObject);
    }

    IEnumerator FollowPlayer(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Camera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = GameObject.Find("Player").transform;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StopCoroutine("FollowPlayer");
            Camera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = GameObject.Find("Player").transform;
        }
        colliderList.Remove(collision.gameObject);
        if (colliderList.Count == 0)
        {
            Door.GetComponent<Animator>().SetBool("Open", false);
            Door.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
