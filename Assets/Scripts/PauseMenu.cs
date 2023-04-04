using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
  public GameObject pausePanel;
  public bool isPaused;

  void Start()
  {
    isPaused = false;
    pausePanel.SetActive(false);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      isPaused = !isPaused;
      pausePanel.SetActive(isPaused);
      Time.timeScale = isPaused ? 0 : 1;
    }
  }
}
