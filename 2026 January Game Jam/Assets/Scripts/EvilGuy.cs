using UnityEngine;
using System.Collections;

public class EvilGuy : MonoBehaviour
{
    public int evilHealth = 5;

    public Transform player;
    public float speed = 3f;
    public float stopDistance = 1.5f;

    public float deathDelay = 1.2f;

    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        // HARD reset animator state
        if (animator != null)
        {
            animator.SetBool("IsDead", false);
            animator.Play("Walk", 0, 0f);
        }
    }

    void Update()
    {
        if (player == null || isDead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.LookAt(player);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bat") && !isDead)
        {
            evilHealth--;

            if (evilHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;

        // Stop physics
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Trigger death animation
        if (animator != null)
        {
            animator.SetBool("IsDead", true);
        }

        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}