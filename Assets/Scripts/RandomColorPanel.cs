using UnityEngine;
using UnityEngine.UI;

public class RandomColorPanel : MonoBehaviour
{
  public Image panelImage; // Assign the UI panel's Image component to this in the Inspector
  private float alpha = 0.4f; // Adjust the alpha value in the Inspector as desired
  private float lastColorChangeTime;

  void Start()
  {
    // Set the initial color of the panel to a random color
    SetRandomColor();
  }

  void Update()
  {
    // Change the color of the panel every 2 seconds
    if (Time.time - lastColorChangeTime >= 2f)
    {
      SetRandomColor();
      lastColorChangeTime = Time.time;
    }
  }

  void SetRandomColor()
  {
    // Generate random RGB values between 0 and 255
    int r = Random.Range(0, 256);
    int g = Random.Range(0, 256);
    int b = Random.Range(0, 256);

    // Create a new color with the random RGB values and the specified alpha
    Color randomColor = new Color(r / 255f, g / 255f, b / 255f, alpha);

    // Set the color of the panel's Image component to the random color
    panelImage.color = randomColor;
  }
}
