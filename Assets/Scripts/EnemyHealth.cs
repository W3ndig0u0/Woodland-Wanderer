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

  void Start()
  {
    currentHealth = maxHealth;
    rb = GetComponent<Rigidbody2D>();
    if (GetComponent<BossHp>() != null)
    {
      bossHp = GetComponent<BossHp>();
      bossHp.UpdateHealthBar(maxHealth);
    }
  }

  public void TakeDamage(int damage, float knockbackForce)
  {
    currentHealth -= damage;
    if (currentHealth <= 0)
    {
      Destroy(gameObject);
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
      TakeDamage(playerAttack.heavyAttackDamage, 30f);
    }

    else if (col.collider.CompareTag("LightAttack"))
    {
      TakeDamage(playerAttack.lightAttackDamage, 10f);
    }
  }
}
