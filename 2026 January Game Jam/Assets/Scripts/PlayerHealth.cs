using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 10;
    public int damageTaken = 1;
    public float damageCooldown = 2f;

    private float damageTimer = 0f;

    void Update()
    {
        if (damageTimer > 0f)
            damageTimer -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && damageTimer <= 0f)
        {
            TakeDamage(damageTaken);
            damageTimer = damageCooldown;
        }
    }

    void TakeDamage(int damage)
    {
        playerHealth -= damage;
        Debug.Log("Player Health: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("Player Died");
            if (playerHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
