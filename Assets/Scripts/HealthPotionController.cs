using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<PlayerCombat>().Health < 3) collision.gameObject.GetComponent<PlayerCombat>().Health++;
            Destroy(gameObject);
        }
    }
}
