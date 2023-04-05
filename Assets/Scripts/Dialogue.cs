using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{

  private string[] lines;
  public bool talking = false;

  private float textSpeed = 0.05f;
  private int index;
  private TextMeshProUGUI text;
  public GameObject panel;

  void Update()
  {
    if (Input.GetButtonDown("Jump"))
    {
      if (text.text == lines[index])
      {
        NextLine();
      }
      else
      {
        StopAllCoroutines();
        text.text = lines[index];
      }
    }
  }

  public void SetLines(string[] newLines)
  {
    lines = newLines;
  }

  public void StartDialogue()
  {
    if (text == null)
    {
      text = GetComponent<TextMeshProUGUI>();
    }

    if (lines.Length == 0)
    {
      lines = new string[1];
      lines[0] = "emm, I do not have anything to say...";
    }
    index = 0;

    text.text = lines[index];

    text.text = string.Empty;

    gameObject.SetActive(true);
    panel.SetActive(true);

    StartCoroutine(TypeLine());
    talking = true;
  }

  IEnumerator TypeLine()
  {
    foreach (var c in lines[index].ToCharArray())
    {
      text.text += c;
      yield return new WaitForSeconds(textSpeed);
    }
  }

  void NextLine()
  {
    if (index < lines.Length - 1)
    {
      index++;
      text.text = string.Empty;
      StartCoroutine(TypeLine());
    }
    else
    {
      talking = false;
      gameObject.SetActive(false);
      panel.SetActive(false);
    }
  }
}
