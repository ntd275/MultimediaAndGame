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

    private float xMove = 0;
    private bool lookRight = true;
    private bool jump = false;
    private bool canJump = true;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal") * Speed;
        animator.SetFloat("Speed", Mathf.Abs(xMove));
        if (Input.GetButtonDown("Jump") && canJump){
            jump = true;
        }
        animator.SetFloat("VelocityY", rg.velocity.y);
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
