using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSound : MonoBehaviour
{
  public new AudioClip chargeAudio;
  public new AudioClip attackAudio;

  public void PlayChargeSound()
  {
    AudioSource.PlayClipAtPoint(chargeAudio, this.gameObject.transform.position);
  }

  public void PlayAttackSound()
  {
    //TODO:Roterar kameran, lägg det en annan plats!!
    CameraShake.instance.Shake(0.3f, 0.15f);
    AudioSource.PlayClipAtPoint(attackAudio, this.gameObject.transform.position);
  }
}
