using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    public GameObject Door;
    private HashSet<GameObject> colliderList = new HashSet<GameObject>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colliderList.Count == 0)
        {
            Door.GetComponent<Animator>().SetBool("Open", true);
            Door.GetComponent<BoxCollider2D>().enabled = false;
        }
        colliderList.Add(collision.gameObject);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        colliderList.Remove(collision.gameObject);
        if (colliderList.Count == 0)
        {
            Door.GetComponent<Animator>().SetBool("Open", false);
            Door.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
