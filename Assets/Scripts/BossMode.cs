using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossMode : MonoBehaviour
{
  public GameObject healthBar;
  public AudioSource bossMusic;
  public AudioSource bgMusic;
  public BossHp bossHp;

  private Color defaultColor = Color.white;

  public Tilemap[] sky;

  void Start()
  {
    healthBar.SetActive(false);
  }

  void Update()
  {
    if (bossHp.isAlive == false)
    {
      BossDead();
    }
  }

  private void BossDead()
  {
    StartCoroutine(BossDeadCoroutine());
  }

  private IEnumerator BossDeadCoroutine()
  {
    healthBar.SetActive(false);
    bossMusic.enabled = false;
    bgMusic.enabled = true;
    foreach (var item in sky)
    {
      yield return StartCoroutine(ChangeTilemapColor(item, defaultColor, 0.1f));
    }
  }

  private void BossStart()
  {
    StartCoroutine(BossStartCoroutine());
  }

  private IEnumerator BossStartCoroutine()
  {
    healthBar.SetActive(true);
    bossMusic.enabled = true;
    bgMusic.enabled = false;
    foreach (var item in sky)
    {
      yield return StartCoroutine(ChangeTilemapColor(item, new Color(1.0f, 0f, 0f, 1.0f), 0.1f));
    }
  }

  private IEnumerator ChangeTilemapColor(Tilemap tilemap, Color targetColor, float duration)
  {
    Color startColor = tilemap.color;
    float elapsedTime = 0;

    while (elapsedTime < duration)
    {
      tilemap.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
      elapsedTime += Time.deltaTime;
      yield return null;
    }

    tilemap.color = targetColor;
  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("Player"))
    {
      BossStart();
    }
  }
}
