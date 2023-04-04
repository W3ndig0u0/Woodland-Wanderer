using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

  public int maxHealth = 100;
  public int currentHealth { get; set; }

  public AudioClip hitAudio;
  public PlayerAttack playerAttack;
  public PlayerMovement playerMovement;
  public PlayerHealth playerHealth;
  public int damageToPlayer;
  private Rigidbody2D rb;
  private BossHp bossHp;
  private Animator animator;

  void Start()
  {
    currentHealth = maxHealth;
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();

    if (GetComponent<BossHp>() != null)
    {
      bossHp = GetComponent<BossHp>();
      bossHp.UpdateHealthBar(maxHealth);
    }
  }

  public void TakeDamage(int damage, float knockbackForce)
  {
    animator.SetTrigger("Damaged");
    currentHealth -= damage;
    if (currentHealth <= 0)
    {
      if (bossHp != null)
      {
        bossHp.isAlive = false;
      }

      animator.SetBool("Dead", true);
      Destroy(gameObject, 0.2f);
    }

    if (bossHp != null)
    {
      Debug.Log(currentHealth);
      bossHp.UpdateHealthBar(currentHealth);
    }

    AudioSource.PlayClipAtPoint(hitAudio, this.gameObject.transform.position);
    Vector2 knockbackDirection = (transform.position - playerMovement.transform.position).normalized;
    rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
  }

  void OnCollisionEnter2D(Collision2D col)
  {

    if (col.collider.CompareTag("Player"))
    {
      playerHealth.damagePlayer(damageToPlayer);
      return;
    }
    else if (col.collider.CompareTag("HeavyAttack"))
    {
      TakeDamage(playerAttack.heavyAttackDamage, 50f);
    }

    else if (col.collider.CompareTag("LightAttack"))
    {
      TakeDamage(playerAttack.lightAttackDamage, 20f);
    }
  }
}
