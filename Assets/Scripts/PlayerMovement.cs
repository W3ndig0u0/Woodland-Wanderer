using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  // !Movement
  private float moveSpeed = 10f;
  private float movementX;

  // !Dash
  private float dashSpeed = 120f;
  private bool isDashing = false;
  private float dashTimer = 0;
  public GameObject dashRange;


  // !Jump
  private float jump = 15f;
  public int maxJumps = 2;
  private int jumpsLeft;
  private bool inAir = false;

  // !Scaling
  public Vector3 ScalePeak;
  public Vector3 ScaleDown;
  private Vector3 ScaleNormal = new Vector3(1f, 1f, 1f);
  public float scalingRate;
  float fallingThreshold = -7f;
  float jumpingSpeedThreshold = 7f;
  bool fallingFast = false;
  


  private int coins = 0;
  private int health = 3;

  private Rigidbody2D rb;

  void Start()
  {
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
    rb.velocity = new Vector2(movementX * moveSpeed, rb.velocity.y);
  }


  private void JumpPlayer()
  {

    if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
    {
      jumpsLeft--;
      rb.velocity = new Vector2(rb.velocity.y, jump);
      inAir = true;
    }
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

  private void FallingSpeedCheck()
  {

    if (rb.velocity.y > jumpingSpeedThreshold)
    {
      fallingFast = false;

    }
    else if (rb.velocity.y < fallingThreshold)
    {
      fallingFast = true;
    }

  }
  private void DashCheck()
  {

    Vector2 dashOffset = new Vector2(rb.position.x + dashSpeed, rb.position.y);
    Vector2 negativeDashOffset = new Vector2(rb.position.x - dashSpeed, rb.position.y);
    Vector2 downDashOffset = new Vector2(rb.position.x, rb.position.y - dashSpeed);

    if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
    {
      rb.position = Vector2.Lerp(rb.position, negativeDashOffset, 6f * Time.deltaTime);
      isDashing = true;
    }

    // ?Går genom marken
    else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) && inAir)
    {
      rb.position = Vector2.Lerp(rb.position, downDashOffset, 6f * Time.deltaTime);
      isDashing = true;
    }

    else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
    {
      rb.position = Vector2.Lerp(rb.position, dashOffset, 6f * Time.deltaTime);
      isDashing = true;
    }

  }

  private void checkHp(){
    if (health <= 0 ){
      Destroy(this);
    }
  }

  // !Reset when landing
  void OnCollisionEnter2D(Collision2D col)
  {
    inAir = false;
    jumpsLeft = maxJumps;
    fallingFast = false;
    // isDashing = false;


    if (col.gameObject.CompareTag("Coin")){ 
      coins++;
      Destroy(col.gameObject);
      Debug.Log("money: " +coins);
    }

    if (col.gameObject.CompareTag("Enemy")){
      health--;
      Destroy(col.gameObject);
      Debug.Log("hp: " + health);
    }
  }
}
