using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMoving : MonoBehaviour
{
  public float speed = 1.5f;
  public float distance = 4f;
  private Vector3 startPos;
  private Vector3 endPos;
  private bool movingToEnd;
  private Animator animator;

  private EnemyFlip flip;

  void Start()
  {
    flip = GetComponent<EnemyFlip>();
    animator = GetComponent<Animator>();
    startPos = transform.position;
    endPos = startPos + distance * Vector3.right;
  }

  void Update()
  {
    Vector3 direction = movingToEnd ? endPos - transform.position : startPos - transform.position;
    transform.position += direction.normalized * speed * Time.deltaTime;
    if (Vector3.Distance(transform.position, endPos) < 0.1f)
    {
      movingToEnd = false;
      FlipSprite();
    }
    else if (Vector3.Distance(transform.position, startPos) < 0.1f)
    {
      movingToEnd = true;
      FlipSprite();
    }
  }

  void FlipSprite()
  {
    Vector3 flip = transform.localScale;
    flip.z *= -1f;
    transform.localScale = flip;
    transform.Rotate(0, 180f, 0f);

  }

  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.collider.CompareTag("Player"))
    {
      flip.LookAtPlayer();
      animator.SetBool("Attack", true);
    }

  }

  void OnCollisionExit2D(Collision2D col)
  {
    if (col.collider.CompareTag("Player"))
    {
      animator.SetBool("Attack", false);
    }
  }

}
