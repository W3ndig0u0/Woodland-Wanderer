using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndLevel : MonoBehaviour
{
  public AudioSource audioSource;

  void Start()
  {
    gameObject.SetActive(false);
  }

  public void ActivateNextLevel()
  {
    gameObject.SetActive(true);
  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("Player"))
    {
      audioSource.Play();
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  }

}
