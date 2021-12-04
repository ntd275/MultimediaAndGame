using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public GameObject AfterJump;
    public bool isGround = true;
    public LayerMask groundLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( (groundLayer.value &(1 << collision.gameObject.layer)) > 0)
        {
            isGround = true;
            Instantiate(AfterJump, transform.parent.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isGround = false;
        }
    }
}
