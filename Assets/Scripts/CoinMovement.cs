using UnityEngine;

public class CoinMovement : MonoBehaviour
{
  private float amplitude = 0.2f;
  private float speed = 1; 

  private float startY;  

  void Start()
  {
    startY = transform.position.y;
  }

  void Update()
  {
    float newY = startY + amplitude * Mathf.Sin(speed * Time.time);
    transform.position = new Vector3(transform.position.x, newY, transform.position.z);
  }
}
