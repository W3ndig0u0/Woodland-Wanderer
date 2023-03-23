using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  // !Movement
  private float moveSpeed = 9f;
  private float movementX;
  public float acceleration, deacceleration;

  // !Dash
  private float dashSpeed = 240f;
  private bool isDashing = false;
  private float dashLeft = 1;

  // !Jump
  private float jump = 80f;
  public int maxJumps = 1;
  private int jumpsLeft;
  private bool inAir = false;


  // !Scaling
  public Vector3 ScalePeak;
  public Vector3 ScaleDown;
  private Vector3 ScaleNormal = new Vector3(1f, 1f, 1f);
  public float scalingRate;
  float jumpingSpeedThreshold = 7f;
  bool fallingFast = false;

  //!Particle
  private ParticleSystem jumpParticle;
  private ParticleSystem jumpPartDark;
  private ParticleSystem damagePart;


  private int coins = 0;
  private int health = 3;

  private Rigidbody2D rb;

  void Start()
  {
    jumpParticle = GameObject.Find("JumpPart").GetComponent<ParticleSystem>();
    jumpPartDark = GameObject.Find("JumpPartDark").GetComponent<ParticleSystem>();
    damagePart = GameObject.Find("DamagePart").GetComponent<ParticleSystem>();

    rb = GetComponent<Rigidbody2D>();
    jumpsLeft = maxJumps;
  }


  void Update()
  {
    PlayerSideMovement();
    FallingSpeedCheck();
    JumpPlayer();
    ResizePlayerJump();
    DashCheck();
    checkHp();
  }


  private void PlayerSideMovement()
  {
    movementX = Input.GetAxisRaw("Horizontal");

    // ?Direkt movement, ingen force
    // rb.velocity = new Vector2(movementX * moveSpeed, rb.velocity.y);

    // !movement med force
    float targetSpeed = movementX * moveSpeed;
    float speedDiff = targetSpeed - rb.velocity.x;
    float velocityPower = 1.20f;
    float accelerationSpeed = (Mathf.Abs(targetSpeed) > 0.05f) ? acceleration : deacceleration;
    float movementSpeed = Mathf.Pow(Mathf.Abs(speedDiff) * accelerationSpeed, velocityPower) * Mathf.Sign(speedDiff);
    rb.AddForce(movementSpeed * Vector2.right);

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

    if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
    {
      // ?Direkt movement, ingen force
      // rb.velocity = new Vector2(rb.velocity.y, jump);
      rb.AddForce((jump * Vector2.up) * 2, ForceMode2D.Impulse);
      jumpsLeft--;

      inAir = true;
    }
  }

  private void DashCheck()
  {

    Vector2 dashOffset = new Vector2(rb.position.x + dashSpeed, rb.position.y);
    Vector2 negativeDashOffset = new Vector2(rb.position.x - dashSpeed, rb.position.y);
    Vector2 downDashOffset = new Vector2(rb.position.x, rb.position.y - (dashSpeed * 0.5f));

    if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A) && dashLeft > 0 && inAir || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) && dashLeft > 0 && inAir)
    {

      rb.MovePosition(rb.position + negativeDashOffset * 10f * Time.deltaTime);

      // rb.AddForce((dashSpeed * Vector2.left) * 3, ForceMode2D.Impulse);
      isDashing = true;
      dashLeft--;
    }

    // ?Går genom marken
    else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S) && dashLeft > 0 && inAir || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) && inAir && dashLeft > 0)
    {
      rb.MovePosition(rb.position + downDashOffset * 10f * Time.deltaTime);
      // rb.AddForce((dashSpeed * Vector2.down) * 3, ForceMode2D.Impulse);

      dashLeft--;
      isDashing = true;
    }

    else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D) && dashLeft > 0 && inAir || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D) && dashLeft > 0 && inAir)
    {
      rb.MovePosition(rb.position + dashOffset * 10f * Time.deltaTime);
      // rb.AddForce((dashSpeed * Vector2.right) * 3, ForceMode2D.Impulse);

      dashLeft--;
      isDashing = true;
    }

  }

  private void FallingSpeedCheck()
  {
    fallingFast = (rb.velocity.y > jumpingSpeedThreshold) ? false : true;
  }

  // !Strech and squash
  private void ResizePlayerJump()
  {
    if (!fallingFast && !isDashing)
    {
      transform.localScale = Vector3.Lerp(transform.localScale, ScalePeak, scalingRate * Time.deltaTime);
    }

    else if (fallingFast && !isDashing)
    {
      transform.localScale = Vector3.Lerp(transform.localScale, ScaleDown, scalingRate * Time.deltaTime);
    }


    // !Scalar tillbaka spelaren till normalt
    if (!inAir)
    {
      transform.localScale = Vector3.Lerp(transform.localScale, ScaleNormal, 7f * Time.deltaTime);
    }

  }

  private void checkHp()
  {
    if (health <= 0)
    {
      Destroy(this.gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("Coin"))
    {
      coins++;
      Destroy(col.gameObject);
      Debug.Log("money: " + coins);
    }
  }


  // !Reset when landing
  void OnCollisionEnter2D(Collision2D col)
  {

    if (col.gameObject.CompareTag("Ground"))
    {
      //! fix if on ground
      jumpParticle.Play();
      jumpPartDark.Play();

      inAir = false;
      fallingFast = false;
      jumpsLeft = maxJumps;
      dashLeft = 1;
    }
    // isDashing = false;


    if (col.gameObject.CompareTag("Enemy"))
    {
      health--;
      //Destroy(col.gameObject);
      Debug.Log("hp: " + health);
      damagePart.Play();
      fallingFast = false;
    }
  }
}
