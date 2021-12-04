using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    public int Health = 3;
    public Animator animator;
    public LayerMask EnemyLayer;
    public float ImortalDuration = 1;

    private float immortalTime = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if((EnemyLayer.value & (1 << collision.gameObject.layer)) > 0 && Time.time > immortalTime){
            Health -= 1;
            immortalTime = Time.time + ImortalDuration;
            animator.SetTrigger("Hit");
            if (Health <= 0)
            {
                GetComponent<PlayerController>().enabled = false;
                enabled = false;
                animator.SetBool("Dead", true);
                Destroy(gameObject, 1);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((EnemyLayer.value & (1 << collision.gameObject.layer)) > 0 && Time.time > immortalTime)
        {
            Health -= 1;
            immortalTime = Time.time + ImortalDuration;
            animator.SetTrigger("Hit");
            if (Health <= 0)
            {
                GetComponent<PlayerController>().enabled = false;
                enabled = false;
                animator.SetBool("Dead", true);
                Destroy(gameObject, 1);
            }
        }
    }
}
