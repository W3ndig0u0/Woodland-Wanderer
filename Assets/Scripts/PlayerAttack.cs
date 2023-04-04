using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  private Animator animator;
  private float lastAttackTime = 0f;
  private float lightAttackCooldown = 0.5f;
  private float heavyAttackCooldown = 1.0f;
  public bool isAttacking = false;

  private PlayerMovement playerMovement;
  private PlayerDash playerDash;

  private bool heavyQueuedAttack = false;
  private bool lightQueuedAttack = false;
  private bool inAttackCooldown = false;
  private float lastLightAttackTime = 0f;
  private float lastHeavyAttackTime = 0f;


  public AudioClip heavyAttackAudio;

  public int lightAttackDamage = 30;
  public int heavyAttackDamage = 120;

  public Collider2D lightAttackHitBox;
  public Collider2D heavyAttackHitBox;

  public Dialogue dialogue;
  public PauseMenu pauseMenu;


  private void Start()
  {
    animator = GetComponent<Animator>();
    playerMovement = GetComponent<PlayerMovement>();
    playerDash = GetComponent<PlayerDash>();
    HeavyAttackDone();
  }

  private void Update()
  {
    float timeSinceLastAttack = Time.time - lastAttackTime;
    bool isMoving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0f || Mathf.Abs(Input.GetAxis("Vertical")) > 0f;

    if (Input.GetKeyDown(KeyCode.J))
    {
      lightQueuedAttack = true;
      heavyQueuedAttack = false;
    }
    else if (Input.GetKeyDown(KeyCode.K))
    {
      lightQueuedAttack = false;
      heavyQueuedAttack = true;
    }

    if (!pauseMenu.isPaused && !dialogue.talking && !inAttackCooldown && !playerDash.isDashing && lightQueuedAttack && Input.GetKeyDown(KeyCode.J) && playerMovement.isGrounded())
    {
      playerMovement.StopMovement();
      animator.SetTrigger("LightAttack");
      isAttacking = true;
      lastAttackTime = Time.time;
      lastLightAttackTime = Time.time; // set lastLightAttackTime to current time
      playerMovement.ResetDrag();
      inAttackCooldown = true;
    }

    else if (!pauseMenu.isPaused && !dialogue.talking && !inAttackCooldown && !playerDash.isDashing && heavyQueuedAttack && Input.GetKeyDown(KeyCode.K) && playerMovement.isGrounded())
    {
      playerMovement.StopMovement();
      animator.SetTrigger("HeavyAttack");
      isAttacking = true;
      lastAttackTime = Time.time;
      lastHeavyAttackTime = Time.time; // set lastHeavyAttackTime to current time
      playerMovement.ResetDrag();
      inAttackCooldown = true;
    }

    if (inAttackCooldown && timeSinceLastAttack >= (lightQueuedAttack ? lightAttackCooldown : heavyAttackCooldown))
    {
      inAttackCooldown = false;
      lastLightAttackTime = 0f; // reset light attack cooldown
      lastHeavyAttackTime = 0f; // reset heavy attack cooldown
    }
    else if (inAttackCooldown && lightQueuedAttack && timeSinceLastAttack >= lightAttackCooldown)
    {
      lastLightAttackTime = 0f; // reset light attack cooldown
    }
    else if (inAttackCooldown && heavyQueuedAttack && timeSinceLastAttack >= heavyAttackCooldown)
    {
      lastHeavyAttackTime = 0f; // reset heavy attack cooldown
    }
  }

  public void HeavyAttackShake()
  {
    CameraShake.instance.Shake(0.2f, 0.15f);
    AudioSource.PlayClipAtPoint(heavyAttackAudio, this.gameObject.transform.position);
    heavyAttackHitBox.enabled = true;
  }

  public void HeavyAttackDone()
  {
    heavyAttackHitBox.enabled = false;
  }

  public void LightAttackShake()
  {
    AudioSource.PlayClipAtPoint(heavyAttackAudio, this.gameObject.transform.position);
    lightAttackHitBox.enabled = true;
  }

  public void LightAttackDone()
  {
    lightAttackHitBox.enabled = false;
  }
}