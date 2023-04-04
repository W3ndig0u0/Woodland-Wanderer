using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

  private int health;
  private int maxHealth = 100;
  public HealthBar healthBar;
  public new AudioClip audio;

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
  void OnTriggerEnter2D(Collider2D col)
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
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  }

}
