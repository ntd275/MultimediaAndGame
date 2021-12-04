using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Sprite Pressed;
    public Sprite UnPressed;
    private HashSet<GameObject> colliderList = new HashSet<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliderList.Add(collision.gameObject);
        GetComponent<SpriteRenderer>().sprite = Pressed;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        colliderList.Remove(collision.gameObject);
        if(colliderList.Count == 0)
        {
            GetComponent<SpriteRenderer>().sprite = UnPressed;
        }
    }
}
