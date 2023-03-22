using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

  public Transform playerTarget;
  public Vector3 offset;

  [Range(1, 10)]
  public float smoothOffset;

  void Update()
  {
    Follow();
  }

  void Follow()
  {
    Vector3 newPosition = playerTarget.position + offset;
    Vector3 smoothPosition = Vector3.Lerp(transform.position, newPosition, smoothOffset * Time.deltaTime);
    transform.position = smoothPosition;
  }
}
