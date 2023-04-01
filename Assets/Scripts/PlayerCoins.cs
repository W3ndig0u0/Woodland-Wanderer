using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoins : MonoBehaviour
{

  private int coins = 0;
  public AmountCoins amountCoins;

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("Coin"))
    {
      coins++;
      amountCoins.SetCoins(coins.ToString());
      Destroy(col.gameObject);
    }
  }
}
