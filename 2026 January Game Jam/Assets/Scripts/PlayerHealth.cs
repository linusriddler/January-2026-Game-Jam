using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int playerHealth = 100;

    public int damageTaken = 33;
    public float damageCooldown = 2f;

    public float knockbackForce = 8f;
    public float upwardKnockback = 2f;

    public Slider healthSlider;

    private float damageTimer = 0f;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = playerHealth;
        }
    }

    void Update()
    {
        if (damageTimer > 0f)
            damageTimer -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && damageTimer <= 0f)
        {
            TakeDamage(damageTaken, collision.transform);
            damageTimer = damageCooldown;
        }
    }

    void TakeDamage(int damage, Transform enemy)
    {
        playerHealth -= damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);

        // Update UI
        if (healthSlider != null)
            healthSlider.value = playerHealth;

        // -------- KNOCKBACK --------
        if (rb != null && enemy != null)
        {
            Vector3 knockbackDir = (transform.position - enemy.position).normalized;
            knockbackDir.y = 0f; // keep player upright

            rb.linearVelocity = Vector3.zero; // reset momentum
            rb.AddForce(knockbackDir * knockbackForce + Vector3.up * upwardKnockback, ForceMode.Impulse);
            rb.AddForce(knockbackDir * knockbackForce + Vector3.back * upwardKnockback, ForceMode.Impulse);
        }
        // ----------------------------

        if (playerHealth <= 0)
        {
            if (WinScreen.instance != null)
                WinScreen.instance.ShowGameOverScreen();

            Destroy(gameObject);
        }
    }
}