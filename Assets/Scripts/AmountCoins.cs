using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmountCoins : MonoBehaviour
{
  public TextMeshProUGUI amount;

  public void SetCoins(string coins)
  {
    amount.text = coins;
  }
}
