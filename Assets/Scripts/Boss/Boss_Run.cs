using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    BossController bc;
    BossCombat bCombat;
    float speed = 3;
    float attackRange = 2;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        rb = animator.GetComponent<Rigidbody2D>();
        bc = animator.GetComponent<BossController>();
        bCombat = animator.GetComponent<BossCombat>();
        speed = bc.Speed;
        attackRange = bCombat.AttackRange * 2;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bc.LookAtPlayer();
        if (Vector2.Distance(rb.position, player.transform.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
            var newPos = Vector2.MoveTowards(rb.position, target, Time.deltaTime * speed);
            rb.MovePosition(newPos);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
