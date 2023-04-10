using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

  public new AudioClip audio;
  public PlayerCoins playerCoins;

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("Player"))
    {
      playerCoins.AddCoins();
      AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);
      Destroy(this.gameObject);
  }
}
