using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour
{
  public HealthBar healthBar;
  public bool isAlive = true;

  public void UpdateHealthBar(int hp)
  {
    healthBar.SetHealth(hp);
  }
}
