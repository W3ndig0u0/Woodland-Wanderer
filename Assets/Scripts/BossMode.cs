using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMode : MonoBehaviour
{
  public GameObject healthBar;
  public AudioSource bossMusic;
  public AudioSource bgMusic;

  void Start()
  {
    healthBar.SetActive(false);
  }


  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("Player"))
    {
      healthBar.SetActive(true);
      bossMusic.enabled = true;
      bgMusic.enabled = false;
    }
  }
}
