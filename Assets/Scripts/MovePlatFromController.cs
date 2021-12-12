using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatFromController : MonoBehaviour
{
    public BoxCollider2D bc;
    public Animator animator;
    public float LimitX;
    public float V;

    private float walked = 0;
    private bool isFacingRight = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(walked) >= LimitX)
        {
            Turn();
        }
        walked += isFacingRight ? V * Time.deltaTime : -V * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + (isFacingRight ? V * Time.deltaTime : -V * Time.deltaTime), transform.position.y);
    }
    private void Turn()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        transform.localScale = theScale;
    }
}
