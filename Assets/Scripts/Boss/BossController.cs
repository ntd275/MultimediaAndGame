using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 5;
    public float DashSpeed = 10;
    public float DashCoolDown = 2;
    private bool isFacingRight = true;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
    }

    public void LookAtPlayer()
    {
        if((isFacingRight && transform.position.x > player.transform.position.x) || (!isFacingRight && transform.position.x < player.transform.position.x))
        {
            Turn();
        }
    }

    private void Turn()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
