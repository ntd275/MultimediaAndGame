using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCheckDrop : MonoBehaviour
{
    public float DropGravity = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().gravityScale = DropGravity;
        }
    }
}
