using UnityEngine;

public class GameOverCamera : MonoBehaviour
{
  public float speed = 0.1f;
  public float resetDistance = 100f;
  private Vector3 startingPosition;

  void Start()
  {
    startingPosition = transform.position;
  }

  void Update()
  {
    Vector3 movement = new Vector3(speed * Time.deltaTime, 0f, speed * Time.deltaTime / 10f); // move camera in x and slightly upwards
    transform.position += movement;

    if (Mathf.Abs(transform.position.x - startingPosition.x) > resetDistance)
    {
      transform.position = startingPosition; // reset camera position to starting position
    }
  }
}
