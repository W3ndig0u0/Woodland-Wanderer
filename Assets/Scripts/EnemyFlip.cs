using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlip : MonoBehaviour
{

  private bool isFlipped = false;
  public Transform player;

  public void LookAtPlayer()
  {
    Vector3 flip = transform.localScale;
    flip.z *= -1f;

    if (transform.position.x > player.position.x && isFlipped)
    {
      transform.localScale = flip;
      transform.Rotate(0, 180f, 0f);
      isFlipped = false;
    }
    else if (transform.position.x < player.position.x && !isFlipped)
    {
      transform.localScale = flip;
      transform.Rotate(0, 180f, 0f);
      isFlipped = true;
    }
  }
}
