using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{

  public AudioSource audioSource;

  public void LoadMenu()
  {

    audioSource.Play();
    SceneManager.LoadScene(0);
  }

  public void NextLevel()
  {
    audioSource.Play();
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void Quit()
  {
    audioSource.Play();
    Application.Quit();
  }
}
