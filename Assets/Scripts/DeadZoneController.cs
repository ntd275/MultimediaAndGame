using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var playerCombat = collision.gameObject.GetComponent<PlayerCombat>();
            playerCombat.TakeDame(1);
            if (playerCombat.Health > 0)
            {
                collision.gameObject.transform.position = transform.GetChild(0).transform.position;
            }
        }
    }
}
