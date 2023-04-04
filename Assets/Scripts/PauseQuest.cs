using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseQuest : MonoBehaviour
{

  public QuestGiver questGiver;
  private TextMeshProUGUI text;
  private string questChoice;

  void Start()
  {
    text = GetComponent<TextMeshProUGUI>();
  }

  void Update()
  {
    string newChoice = questGiver.GetQuestChoice();
    if (questChoice != newChoice)
    {
      questChoice = newChoice;
      text.text = questChoice;
    }
  }
}
