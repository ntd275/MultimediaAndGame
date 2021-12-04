using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public Animator animator;
    public float LimitX;
    public float V;

    private float walked = 0;
    private bool isDead = false;
    private bool isFacingRight = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("hit");
            isDead = true;
            V = 0;
            Destroy(gameObject, 1f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            bc.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(walked) >= LimitX)
        {
            Turn();
        }
        walked += isFacingRight ? V * Time.deltaTime : -V * Time.deltaTime;
        rb.MovePosition(new Vector2(rb.position.x + (isFacingRight ? V * Time.deltaTime : -V * Time.deltaTime), rb.position.y));
    }
    private void Turn()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
