using System.Collections;
using UnityEngine;

public class WitchRun : StateMachineBehaviour
{
  private Transform player;
  private Rigidbody2D rb;
  private float speed = 1.5f;
  private float playerRange = 20f;
  EnemyFlip enemyFlip;

  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    rb = animator.GetComponent<Rigidbody2D>();
    enemyFlip = animator.GetComponent<EnemyFlip>();

  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    enemyFlip.LookAtPlayer();

    if (Vector2.Distance(player.position, rb.position) <= playerRange)
    {
      Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
      Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
      animator.SetFloat("Speed", speed);
      rb.MovePosition(newPosition);
    }
  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (Vector2.Distance(player.position, rb.position) > playerRange)
    {
      animator.SetFloat("Speed", 0);
    }


  }
}