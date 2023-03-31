using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
  public Collider2D hitboxCollider;

  public void EnableHitbox()
  {
    hitboxCollider.enabled = true;
  }

  public void DisableHitbox()
  {
    hitboxCollider.enabled = false;
  }
}
