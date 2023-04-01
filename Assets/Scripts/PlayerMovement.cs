using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  // !Movement
  private float moveSpeed = 2.5f;
  private float movementX;
  private bool isFacingRight = false;

  // !Dash
  private float dashPower = 17f;
  private float dashingTime = 0.2f;
  private float dashCoolDown = 0.7f;
  private bool isDashing = false;
  private bool canDash = true;
  [SerializeField] private TrailRenderer trail;


  // !Jump
  private float jump = 7f;
  public int maxJumps = 1;
  private int jumpsLeft;
  private bool inAir = false;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private LayerMask groundLayer;

  //!Animation
  public Animator animation;
  private Rigidbody2D rb;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    jumpsLeft = maxJumps;
  }


  void Update()
  {
    if (isDashing)
    {
      return;
    }

    PlayerSideMovement();
    JumpPlayer();
    DashCheck();
    Flip();
    movementX = Input.GetAxisRaw("Horizontal");
    animation.SetFloat("Speed", Mathf.Abs(movementX));
  }

  void FixedUpdate()
  {

    if (isDashing)
    {
      return;
    }

    // ?Direkt movement, ingen force
    rb.velocity = new Vector2(movementX * moveSpeed, rb.velocity.y);
  }

  //!VÄnder karaktären
  private void Flip()
  {
    if (isFacingRight && movementX < 0f || !isFacingRight && movementX > 0f)
    {
      isFacingRight = !isFacingRight;
      Vector3 localScale = transform.localScale;
      localScale.x *= -1f;
      transform.localScale = localScale;
    }
  }

  private bool isGrounded()
  {
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
  }


  private void PlayerSideMovement()
  {

    // !movement med force
    //float targetSpeed = movementX * moveSpeed;
    //float speedDiff = targetSpeed - rb.velocity.x;
    //float velocityPower = 1.20f;
    //float accelerationSpeed = (Mathf.Abs(targetSpeed) > 0.05f) ? acceleration : deacceleration;
    //float movementSpeed = Mathf.Pow(Mathf.Abs(speedDiff) * accelerationSpeed, velocityPower) * Mathf.Sign(speedDiff);
    //rb.AddForce(movementSpeed * Vector2.right);

    // !Bromsar spelaren när den vänder
    if (!inAir && Mathf.Abs(movementX) < 0.01f)
    {
      float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), MathF.Abs(0.3f));
      amount *= MathF.Sign(rb.velocity.x);
      rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
    }

  }

  private void JumpPlayer()
  {

    if (isDashing)
    {
      return;
    }

    if (Input.GetButtonDown("Jump") && jumpsLeft > 0 && isGrounded())
    {
      // ?Direkt movement, ingen force
      rb.velocity = new Vector2(rb.velocity.x, jump);
      //rb.AddForce((jump * Vector2.up) * 2, ForceMode2D.Impulse);
      jumpsLeft--;
      inAir = true;
    }

    //? Längre hopp ju längre man håller
    if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
    {
      // ?Direkt movement, ingen force
      rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }
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

  // !Reset when landing
  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.CompareTag("Ground"))
    {
      //jumpParticle.Play();
      //jumpPartDark.Play();

      inAir = false;
      jumpsLeft = maxJumps;
    }
  }
}
