using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    // Start is called before the first frame update
    public int Health;
    public Animator animator;
    public CapsuleCollider2D cc;
    public Rigidbody2D rb;
    public Transform AttackPoint;
    public Transform AttackPoint2;
    public Transform AttackPoint3;
    public Transform AttackPoint4;
    public float AttackRange = 2;
    public LayerMask PlayerLayer;
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDame(int dame)
    {
        Health -= dame;
        if(Health <= 0)
        {
            cc.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.Play("Death");
            GetComponent<BossController>().enabled = false;
        }
    }

    public void Attack1()
    {
        var hitPlayers= Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayer);
        foreach (var enemy in hitPlayers)
        {
            enemy.GetComponent<PlayerCombat>().TakeDame(1);
        }
        hitPlayers = Physics2D.OverlapCircleAll(AttackPoint2.position, AttackRange, PlayerLayer);
        foreach (var enemy in hitPlayers)
        {
            enemy.GetComponent<PlayerCombat>().TakeDame(1);
        }
    }

    public void Attack2()
    {
        var hitPlayers = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange*0.75f, PlayerLayer);
        foreach (var enemy in hitPlayers)
        {
            enemy.GetComponent<PlayerCombat>().TakeDame(1);
        }
        hitPlayers = Physics2D.OverlapCircleAll(AttackPoint2.position, AttackRange*0.75f, PlayerLayer);
        foreach (var enemy in hitPlayers)
        {
            enemy.GetComponent<PlayerCombat>().TakeDame(1);
        }

        hitPlayers = Physics2D.OverlapCircleAll(AttackPoint3.position, AttackRange * 0.5f, PlayerLayer);
        foreach (var enemy in hitPlayers)
        {
            enemy.GetComponent<PlayerCombat>().TakeDame(1);
        }

        hitPlayers = Physics2D.OverlapCircleAll(AttackPoint4.position, AttackRange * 0.5f, PlayerLayer);
        foreach (var enemy in hitPlayers)
        {
            enemy.GetComponent<PlayerCombat>().TakeDame(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        Gizmos.DrawWireSphere(AttackPoint2.position, AttackRange);
        Gizmos.DrawWireSphere(AttackPoint3.position, AttackRange * 0.5f);
        Gizmos.DrawWireSphere(AttackPoint4.position, AttackRange * 0.5f);
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange*0.75f);
        Gizmos.DrawWireSphere(AttackPoint2.position, AttackRange*0.75f);
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange*1.25f);
    }
}
