using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCheckAttack : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.parent.gameObject.GetComponent <BlueFlyController>().needAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.parent.gameObject.GetComponent<BlueFlyController>().needAttack = false;
        }
    }
}
