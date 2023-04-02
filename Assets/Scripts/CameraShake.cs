using UnityEngine;

public class CameraShake : MonoBehaviour
{

  public float shakeDuration;
  private float shakePower;
  private float shakeFade;
  private float shakeRotation;

  public float rotationMultiplier = 7.5f;
  public static CameraShake instance;

  void Start()
  {
    instance = this;
  }

  void LateUpdate()
  {
    if (shakeDuration > 0)
    {
      shakeDuration -= Time.deltaTime;

      float xAmount = Random.Range(-1f, 1f) * shakePower;
      float yAmount = Random.Range(-1f, 1f) * shakePower;

      transform.position += new Vector3(xAmount, yAmount, 0f);
      shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFade * Time.deltaTime);
      shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFade * Time.deltaTime * rotationMultiplier);
    }
    transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
  }

  public void Shake(float length, float power)
  {
    shakeDuration = length;
    shakePower = power;
    shakeFade = power / length;

    shakeRotation = power * rotationMultiplier;
  }
}
