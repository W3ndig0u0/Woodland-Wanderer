using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
  public int coins;
  public AmountCoins amountCoins;

  void Start()
  {
    coins = 0;
  }

  public void AddCoins()
  {
    coins++;
    amountCoins.SetCoins(coins.ToString());
  }
}
