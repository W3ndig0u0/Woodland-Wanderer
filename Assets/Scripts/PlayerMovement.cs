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
  private PlayerSlide playerSlide;


  // !Jump
  private float jump = 7f;
  public int maxJumps = 1;
  private int jumpsLeft;
  private bool inAir = false;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private AudioClip jumpAudio;

  //!Animation
  public new Animator animation;
  private Rigidbody2D rb;

  private PlayerAttack playerAttack;
  private float originalDrag;
  private bool stopMovement;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    jumpsLeft = maxJumps;
    playerAttack = GetComponent<PlayerAttack>();
    playerSlide = GetComponent<PlayerSlide>();
    originalDrag = rb.drag;
  }

  void Update()
  {
    if (playerAttack.isAttacking)
    {
      return;
    }

    if (playerSlide.isDashing)
    {
      return;
    }

    PlayerSideMovement();
    JumpPlayer();
    Flip();
    movementX = Input.GetAxisRaw("Horizontal");
    animation.SetFloat("Speed", Mathf.Abs(movementX));
  }

  void FixedUpdate()
  {

    if (playerSlide.isDashing || stopMovement)
    {
      return;
    }

    // ?Direkt movement, ingen force
    rb.velocity = new Vector2(movementX * moveSpeed, rb.velocity.y);
  }

  //!Reseta dragen efter attacken
  public void ResetDrag()
  {
    rb.drag = originalDrag;
    stopMovement = false;
    moveSpeed = 2.5f;
  }

  public bool isGrounded()
  {
    return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
  }

  //!Stoppa när man atteckerar
  public void StopMovement()
  {
    rb.velocity = Vector2.zero;
    rb.angularVelocity = 0f;
    moveSpeed = 0f;
    movementX = 0f;
    animation.SetFloat("Speed", 0f);
    rb.drag = 100000;
    rb.MovePosition(transform.position);
    stopMovement = true;
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

  private void PlayerSideMovement()
  {
    // !Bromsar spelaren när den vänder
    if (!inAir && Mathf.Abs(movementX) < 0.01f)
    {
      float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), MathF.Abs(0.2f));
      amount *= MathF.Sign(rb.velocity.x);
      rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
    }

  }

  private void JumpPlayer()
  {

    if (Input.GetButtonDown("Jump") && jumpsLeft > 0 && isGrounded())
    {
      rb.velocity = new Vector2(rb.velocity.x, jump);
      //rb.AddForce((jump * Vector2.up) * 2, ForceMode2D.Impulse);
      AudioSource.PlayClipAtPoint(jumpAudio, this.gameObject.transform.position);
      jumpsLeft--;
      inAir = true;
    }

    //? Längre hopp ju längre man håller
    if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
    {
      rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }
  }


  // !Reset when landing
  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.CompareTag("Ground"))
    {

      inAir = false;
      jumpsLeft = maxJumps;
    }
  }
}
