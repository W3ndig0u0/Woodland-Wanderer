using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
  private EnemyFlip enemyFlip;
  public Dialogue dialogue;
  public PlayerCoins playerCoins;
  public string[] lines;

  public int questRecive;
  private string[] questChoice;

  public bool tutorialMode = false;
  public bool isQuestGiver = false;
  public bool questComplete = false;

  [HideInInspector] public bool playerRecivedQuest = false;

  public int coinsToCollect = 0;
  public int enemiesToDefeat = 0;

  private int questIndex;
  public BossHp bossHp;
  public EndLevel endLevel;

  private bool canInteract = false;


  void Start()
  {
    enemyFlip = GetComponent<EnemyFlip>();
    questChoice = new string[4];
    questChoice[0] = $"Thou hast not yet received a quest.";
    questChoice[3] = $"Thou hast completed thine quest.";

    if (bossHp == null)
    {
      return;
    }
  }

  void Update()
  {
    enemyFlip.LookAtPlayer();

    if (playerRecivedQuest && !questComplete)
    {
      QuestProgress();
    }

    if (questComplete)
    {
      questRecive = 3;
      Debug.Log(questChoice[questRecive]);
      endLevel.ActivateNextLevel();
    }

    Interact();
  }

  private void Interact()
  {
    if (canInteract && Input.GetKeyDown(KeyCode.W) && !tutorialMode)
    {
      dialogue.SetLines(lines);
      dialogue.StartDialogue();

      if (isQuestGiver)
      {
        Quest();
      }
    }
  }

  public string GetQuestChoice()
  {
    return questChoice[questIndex];
  }

  void Quest()
  {
    questIndex = questRecive;
    playerRecivedQuest = true;
    questChoice[1] = $"Collect {coinsToCollect} amount of gold.";
    questChoice[2] = $"Defeat the Evil Witch And Collect {coinsToCollect} amount of gold.";
    Debug.Log(questChoice[questRecive]);
  }

  void QuestProgress()
  {
    if (questRecive == 1 && coinsToCollect <= playerCoins.coins)
    {
      questComplete = true;
      Debug.Log("DONE");
    }

    //?BossHP blir null när bossen dör.
    else if (questRecive == 2 && coinsToCollect <= playerCoins.coins && bossHp == null)
    {
      questComplete = true;
      Debug.Log("DONE");
    }
  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("Player"))
    {
      canInteract = true;
    }

    if (tutorialMode && canInteract)
    {
      dialogue.SetLines(lines);
      dialogue.StartDialogue();

      if (isQuestGiver)
      {
        Quest();
      }
    }
  }

  private void OnTriggerExit2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("Player"))
    {
      canInteract = false;
    }
  }
}
