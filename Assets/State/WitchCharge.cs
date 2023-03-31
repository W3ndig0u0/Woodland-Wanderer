using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchCharge : StateMachineBehaviour
{
  private Transform player;
  private Rigidbody2D rb;
  private float attackRange = 2f;
  private float chargeRange = 10f;
  private EnemyFlip enemyFlip;

  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    rb = animator.GetComponent<Rigidbody2D>();
    enemyFlip = animator.GetComponent<EnemyFlip>();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    enemyFlip.LookAtPlayer();

    if (Vector2.Distance(player.position, rb.position) <= attackRange)
    {
      animator.SetBool("AttackRange", true);

      if (animator.GetBool("AttackRange"))
      {
        animator.SetTrigger("Attack");
      }
    }
    else if (Vector2.Distance(player.position, rb.position) <= chargeRange)
    {
      animator.SetBool("AttackRange", false);
    }
  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    animator.ResetTrigger("Attack");
  }
}
