using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

  private int health;
  private int maxHealth = 100;
  public HealthBar healthBar;
  public AudioClip audio;

  void Start()
  {
    health = maxHealth;
    healthBar.SetMaxHealth(maxHealth);
  }

  void Update()
  {
    CheckHp();
  }

  public void damagePlayer(int damage)
  {
    health -= damage;
    healthBar.SetHealth(health);
    AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);
  }

  // !Reset when landing
  void OnCollisionEnter2D(Collision2D col)
  {
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
      //Application.LoadLevel(Application.loadedLevel);
    }
  }

}
