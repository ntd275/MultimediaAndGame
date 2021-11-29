using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 1000;
    public Rigidbody2D rg;
    public Animator animator;
    public float JumpForce = 600;
    public GameObject BeforeJump;
    public GameObject AfterJump;
    public float DashSpeed = 8000;
    public float DashDuration = 0.5f;
    public float DashCoolDown = 1;
    public float AfterImageCoolDown = 0.1f;

    private float xMove = 0;
    private bool lookRight = true;
    private bool jump = false;
    private bool canJump = true;

    private float dashTime = 0;
    private float nextTimeCanDash = 0;
    private float nextTimePrintAfterImage = 0;
    private float now;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal") * Speed;
        if (Input.GetButtonDown("Jump") && canJump){
            jump = true;
        }
        animator.SetFloat("Speed", Mathf.Abs(xMove));
        animator.SetFloat("VelocityY", rg.velocity.y);
        now = Time.time;
        if (Input.GetButtonDown("Dash") && now > nextTimeCanDash)
        {
            dashTime = now + DashDuration;
            nextTimeCanDash = dashTime + DashCoolDown;
            animator.SetTrigger("Dash");
        }

        if (now < dashTime && now > nextTimePrintAfterImage)
        {
            nextTimePrintAfterImage = now + AfterImageCoolDown;
            GameObject trailPart = new GameObject();
            SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
            trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
            trailPart.transform.position = transform.position;
            trailPart.transform.localScale = transform.localScale;
            Destroy(trailPart, AfterImageCoolDown);
        }
    }

    private void FixedUpdate()
    {
        if((xMove > 0 && !lookRight) || (xMove <0 && lookRight))
        {
            Flip();
        }
        rg.velocity = new Vector2(xMove * Time.fixedDeltaTime, rg.velocity.y);

        if(jump)
        {
            rg.AddForce(new Vector2(0, JumpForce));
            jump = false;
            canJump = false;
            Instantiate(BeforeJump, transform.position, Quaternion.identity);

        }

        now = Time.time;
        if (now < dashTime)
        {
            if (lookRight)
            {
                rg.velocity = new Vector2(DashSpeed * Time.fixedDeltaTime, rg.velocity.y);
            }
            else
            {
                rg.velocity = new Vector2(-DashSpeed * Time.fixedDeltaTime, rg.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canJump)
        {
            Instantiate(AfterJump, transform.position, Quaternion.identity);
        }
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }

    private void Flip()
    {
        lookRight = !lookRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}
