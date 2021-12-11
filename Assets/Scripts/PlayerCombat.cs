using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    public int Health = 3;
    public Animator animator;
    public LayerMask CanTakeDamage;
    public LayerMask EnemyLayer;
    public LayerMask SwitchLayer;
    public float ImortalDuration = 1;
    public Transform AttackPoint;
    public float AttackRange = 1;
    public float AttackRate = 2;
    public AudioSource AttackSound;
    public AudioSource HitSound;
    public AudioSource DieSound;

    private float immortalTime = 0;
    private float attackInput = 0;
    private float nextAttackTime = 0;
    private float now = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        now = Time.time;
        attackInput = Input.GetAxisRaw("Attack");
        if(attackInput >= 0.1 && now > nextAttackTime)
        {
            AttackSound.Play();
            nextAttackTime = now + 1f / AttackRate;
            Attack();
        }
        
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        var hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayer);
        foreach(var enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHit>().Hit(1);
        }
        var hitSwitch = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, SwitchLayer);
        foreach (var s in hitSwitch)
        {
            s.GetComponent<SwitchController>().IsOn = !s.GetComponent<SwitchController>().IsOn;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if((CanTakeDamage.value & (1 << collision.gameObject.layer)) > 0 && Time.time > immortalTime){
            Health -= 1;
            immortalTime = Time.time + ImortalDuration;
            animator.SetTrigger("Hit");
            HitSound.Play();
            if (Health <= 0)
            {
                GetComponent<PlayerController>().enabled = false;
                enabled = false;
                animator.SetBool("Dead", true);
                DieSound.Play();
                StartCoroutine(Restart(1));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((CanTakeDamage.value & (1 << collision.gameObject.layer)) > 0 && Time.time > immortalTime)
        {
            Health -= 1;
            immortalTime = Time.time + ImortalDuration;
            animator.SetTrigger("Hit");
            HitSound.Play();
            if (Health <= 0)
            {
                GetComponent<PlayerController>().enabled = false;
                enabled = false;
                animator.SetBool("Dead", true);
                DieSound.Play();
                StartCoroutine(Restart(1));
            }
        }
    }

    IEnumerator Restart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
