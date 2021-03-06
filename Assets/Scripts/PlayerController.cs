using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject BeforeJump;
    public GameObject AfterJump;
    public LayerMask StoneLayer;
    public BoxCollider2D bc;
    public LayerMask GroundLayer;
    public AudioSource JumpSound;
    public AudioSource RunSound;
    public AudioSource DashSound;

    public float MoveSpeed = 1000;
    public float GroundAccelerationRate;
    public float GroundDecelerationRate;
    public float AirAccelerationRate;
    public float AirDecelerationRate;
    public float VelocityPower;
    public float Friction;

    public float JumpCoyoteTime = 0.1f;
    public float JumpForce;
    public float JumpCutMultiple;
    public float GravityScale = 1;
    public float GravityFall = 3;

    public float DashSpeed = 50;
    public float DashCoolDown = 1;
    public float AfterImageTime = 0.5f;
    public float AfterImageCoolDown = 0.1f;

    private Vector2 moveInput;
    private Vector2 lastMoveInput;
    private bool isFacingRight = true;
    private float runSoundPlayTime = 0;

    private float jumpInput;
    private float lastGroundedTime = 0;
    private bool jump = false;
    private bool isJumpingUp = false;

    private float dashInput;
    private bool dash = false;
    private float beginDash = 0;
    private float nextTimeCanDash = 0;
    private float nextTimePrintAfterImage = 0;
    private float now;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        now = Time.time;
        //Input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetAxisRaw("Jump");
        dashInput = Input.GetAxisRaw("Dash");

        //Turn
        if (moveInput.x != 0)
        {
            lastMoveInput.x = moveInput.x;
        }

        if(moveInput.y != 0)
        {
            lastMoveInput.y = moveInput.y;
        }

        if( (lastMoveInput.x > 0 && !isFacingRight) || (lastMoveInput.x < 0 && isFacingRight))
        {
            Turn();
        }

        //Ground check
        if (IsGround())
        {
            lastGroundedTime = now;
        }

        if (rb.velocity.y <= 0)
        {
            isJumpingUp = false; 
        }

        if (jumpInput > 0.01f && lastGroundedTime + JumpCoyoteTime > now && !isJumpingUp)
        {
            jump = true;
        }

        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        animator.SetFloat("VelocityY", rb.velocity.y);

        if (dashInput > 0.01f && now > nextTimeCanDash)
        {
            dash = true;
            beginDash = now;
            nextTimeCanDash = now + DashCoolDown;
            animator.SetTrigger("Dash");
        }

        if (beginDash + AfterImageTime > now && now > nextTimePrintAfterImage)
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
        //Move
        float targetSpeed = moveInput.x * MoveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate;
        if(lastGroundedTime + JumpCoyoteTime > now)
        {
            //Grounded
            accelRate = Mathf.Abs(targetSpeed) > 0.01 ? GroundAccelerationRate : GroundDecelerationRate;
        }
        else
        {
            accelRate = Mathf.Abs(targetSpeed) > 0.01 ? AirAccelerationRate : AirDecelerationRate;
        }
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, VelocityPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
        if(moveInput.x >= 0.01f && now > runSoundPlayTime)
        {
            runSoundPlayTime = now + 0.3f;
            //RunSound.Play();
        }

        //Friction
        if (lastGroundedTime + JumpCoyoteTime > now && !isJumpingUp && Mathf.Abs(moveInput.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(Friction)) * Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        //Jump
        if (jump)
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            Instantiate(BeforeJump, transform.position, Quaternion.identity);
            JumpSound.Play();
            jump = false;
            isJumpingUp = true;
        }

        //JumpCut
        if(jumpInput < 0.01f && isJumpingUp)
        {
            rb.AddForce(Vector2.down * rb.velocity.y * (1 - JumpCutMultiple), ForceMode2D.Impulse);
        }

        //Jump Gravity

        if (rb.velocity.y <= 0 && lastGroundedTime + JumpCoyoteTime <= now)
        {
            rb.gravityScale = GravityFall;
        }
        else
        {
            rb.gravityScale = GravityScale;
        }

        //Dash
        if (dash)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            float tagetDashSpeed =  DashSpeed;
            if(!isFacingRight)
            {
                tagetDashSpeed = -tagetDashSpeed;
            }
            float speedDiff = tagetDashSpeed - rb.velocity.x;
            rb.AddForce(speedDiff * Vector2.right,ForceMode2D.Impulse);
            DashSound.Play();
            dash = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(AfterJump, transform.position, Quaternion.identity);
        if (collision.gameObject.tag == "Stone")
        {
            var front = Physics2D.Raycast(bc.bounds.center, isFacingRight ? Vector2.right : Vector2.left, bc.bounds.extents.x + 0.1f, StoneLayer);
            //Debug.DrawRay(box.bounds.center, (isFacingRight ? Vector2.right : Vector2.left)*(box.bounds.extents.x + 0.1f), Color.green,10);
            if(front.collider != null)
            {
                animator.SetBool("Push", true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stone")
        {
            animator.SetBool("Push", false);
        }
    }

    private bool IsGround()
    {
        var bottomMiddle = Physics2D.Raycast(bc.bounds.center, Vector2.down, bc.bounds.extents.y + 0.1f, GroundLayer);
        if(bottomMiddle.collider != null)
        {
            return true;
        }
        var bottomLeft = Physics2D.Raycast(new Vector2(bc.bounds.center.x - bc.bounds.extents.x,bc.bounds.center.y), Vector2.down, bc.bounds.extents.y + 0.1f, GroundLayer);
        if (bottomLeft.collider != null)
        {
            return true;
        }
        var bottomRight = Physics2D.Raycast(new Vector2(bc.bounds.center.x + bc.bounds.extents.x, bc.bounds.center.y), Vector2.down, bc.bounds.extents.y + 0.1f, GroundLayer);
        if (bottomRight.collider != null)
        {
            return true;
        }
        return false;
    }

    private void Turn()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
