using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


  private int health;
  private int maxHealth = 3;
  public HealthBar healthBar;

  void Start()
  {
    health = maxHealth;
    healthBar.SetMaxHealth(maxHealth);
  }

  void Update()
  {
    CheckHp();
  }

  // !Reset when landing
  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.CompareTag("Enemy"))
    {
      health--;
      //Destroy(col.gameObject);
      healthBar.SetHealth(health);

      Debug.Log("hp: " + health);
    }

    if (col.gameObject.CompareTag("Killzone"))
    {
      health -= maxHealth;
      healthBar.SetHealth(health);
    }
  }


  private void CheckHp()
  {
    if (health <= 0)
    {
      Application.LoadLevel(Application.loadedLevel);
    }
  }

}
