using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
  private float dashPower = 17f;
  private float dashingTime = 0.2f;
  private float dashCoolDown = 0.7f;
  public bool isDashing = false;
  private bool canDash = true;
  [SerializeField] private TrailRenderer trail;
  private Rigidbody2D rb;
  private PlayerAttack playerAttack;

  public PauseMenu pauseMenu;

  void Start()
  {
    playerAttack = GetComponent<PlayerAttack>();
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    if (pauseMenu.isPaused || isDashing || playerAttack.isAttacking)
    {
      return;
    }

    DashCheck();
  }

  private void DashCheck()
  {
    if (Input.GetKey(KeyCode.LeftShift) && canDash)
    {
      StartCoroutine(Dash());
    }
  }

  private IEnumerator Dash()
  {
    canDash = false;
    isDashing = true;
    float ogGravity = rb.gravityScale;
    rb.gravityScale = 0f;
    rb.velocity = new Vector2(transform.localScale.x * -dashPower, 0f);
    trail.emitting = true;
    yield return new WaitForSeconds(dashingTime);
    trail.emitting = false;
    rb.gravityScale = ogGravity;
    isDashing = false;
    yield return new WaitForSeconds(dashCoolDown);
    canDash = true;
  }

  // private IEnumerator Slide()
  // {
  //   canSlide = false;
  //   isSliding = true;
  //   float ogGravity = rb.gravityScale;
  //   rb.gravityScale = 0f;
  //   rb.velocity = new Vector2(transform.localScale.x * slidePower, 0f);
  //   trail.emitting = true;
  //   animator.SetBool("isSliding", true);
  //   yield return new WaitForSeconds(time);
  //   rb.gravityScale = ogGravity;
  //   trail.emitting = false;
  //   animator.SetBool("isSliding", false);
  //   rb.gravityScale = ogGravity;
  //   isSliding = false;

  //   yield return new WaitForSeconds(coolDown);
  //   canSlide = true;
  //}
}