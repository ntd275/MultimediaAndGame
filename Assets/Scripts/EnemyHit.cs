using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public int Heath = 1;
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Hit(int damage)
    {
        animator.SetTrigger("hit");
        Heath -= damage;
        foreach(var bc in gameObject.GetComponents<BoxCollider2D>())
        {
            bc.enabled = false;
        }
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject, 1f);
    }
}
