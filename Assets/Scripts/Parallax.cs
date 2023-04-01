using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{

  private float length;
  private float startPosition;
  public new GameObject camera;
  public float paraleaxEffect;
  public float smoothness = 3.0f;

  void Start()
  {
    startPosition = transform.position.x;
    length = (GetComponent<Tilemap>().size.x * GetComponent<Tilemap>().cellSize.x) / 3;
  }

  void Update()
  {
    float temp = (camera.transform.position.x * (1 - paraleaxEffect));
    float distance = (camera.transform.position.x * paraleaxEffect);
    Vector3 targetPosition = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

    if (temp > startPosition + length)
    {
      startPosition += length;
      targetPosition.x += length;
    }
    else if (temp < startPosition - length)
    {
      startPosition -= length;
      targetPosition.x -= length;
    }

    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothness);

  }
}