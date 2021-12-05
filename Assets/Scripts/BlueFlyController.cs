using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlueFlyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public Animator animator;
    public float LimitX;
    public float V;
    public float attackSpeed;
    public float goBackSpeed;
    public float attackWait;
    public bool needAttack;
    public Vector2 attackTarget;
    public float LimitAttackTime;
    public float LimitAttackDistance;

    private float walked = 0;
    private bool isDead = false;
    private bool isFacingRight = false;
    private Vector3 center;
    private float nextAttack = 0;
    private int state = 0;   // 0: idle, 1: attacking, 2: going back center
    private float attackTime;

    private const int IDLE = 0;
    private const int ATTACKING = 1;
    private const int GOBACK = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        center = transform.position;
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(state == GOBACK)
        {
            bc.isTrigger = true;
        }

        if(collision.gameObject.tag == "Player")
        {
            if (state == ATTACKING)
            {
                animator.SetBool("isAttack", false);
                state = GOBACK;
            }
        }

    }

    private void FixedUpdate()
    {
        if (needAttack && nextAttack <= 0 && state == IDLE)
        {
            animator.SetBool("isAttack", true);
            attackTime = 0;
            nextAttack = attackWait;
            state = ATTACKING;
        }

        switch (state)
        {
            case GOBACK:
                nextAttack -= Time.deltaTime;
                if (nextAttack < 0) nextAttack = 0;
                rb.MovePosition(Vector2.MoveTowards(transform.position, center, goBackSpeed * Time.deltaTime));
                if ((transform.position.x > center.x && isFacingRight) || (transform.position.x < center.x && !isFacingRight))
                {
                    Turn();
                }
                if (transform.position == center)
                {
                    state = IDLE;
                    if (bc.isTrigger)
                    {
                        bc.isTrigger = false;
                    }
                }
                return;
            case IDLE:
                nextAttack -= Time.deltaTime;
                if (nextAttack < 0) nextAttack = 0;
                break;

            case ATTACKING:
                attackTime += Time.deltaTime;
                if(attackTime >= LimitAttackTime || Vector2.Distance(transform.position, center) > LimitAttackDistance)
                {
                    animator.SetBool("isAttack", false);
                    state = GOBACK;
                    return;
                }
                attackTarget = GameObject.Find("Player").transform.position;
                rb.MovePosition(Vector2.MoveTowards(transform.position, attackTarget, attackSpeed * Time.deltaTime));
                if((transform.position.x > attackTarget.x && isFacingRight) || (transform.position.x < attackTarget.x && !isFacingRight))
                {
                    Turn();
                }
                return;
        }


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
