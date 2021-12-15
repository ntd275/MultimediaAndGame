using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun_Stage2 : StateMachineBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    BossController bc;
    BossCombat bCombat;
    float speed = 3;
    float attackRange = 2;
    float nextTimeCanDash = 0;
    float dashCoolDown;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        rb = animator.GetComponent<Rigidbody2D>();
        bc = animator.GetComponent<BossController>();
        bCombat = animator.GetComponent<BossCombat>();
        speed = bc.Speed*1.5f;
        dashCoolDown = bc.DashCoolDown;
        attackRange = bCombat.AttackRange * 2;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bc.LookAtPlayer();
        var distaince = Vector2.Distance(rb.position, player.transform.position);
        if(distaince > attackRange*2.5f && distaince < attackRange * 3 && Time.time > nextTimeCanDash)
        {
            nextTimeCanDash = Time.time + dashCoolDown;
            animator.SetTrigger("Dash_Stage2");
            return;
        }


        if (distaince <= attackRange)
        {
            var x = Random.Range(0f, 3f);
            if (x < 1.5f)
            {
                animator.SetTrigger("Attack2_Stage2");
            }
            else
            {
                animator.SetTrigger("Attack_Stage2");
            }
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
        animator.ResetTrigger("Attack_Stage2");
        animator.ResetTrigger("Attack2_Stage2");
        animator.ResetTrigger("Dash_Stage2");
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
