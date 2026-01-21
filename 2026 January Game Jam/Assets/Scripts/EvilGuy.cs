using UnityEngine;

public class EvilGuy : MonoBehaviour
{
    public int evilHealth = 3;
    public Transform player;
    public float speed = 3f;
    public float stopDistance = 1.5f;
    public float collision;

    private void Start()
    {
        evilHealth = 5;
    }
    void Update()
    {
        if (player == null) return;

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
        if (collision.gameObject.CompareTag("Bat"))
        {
            evilHealth -= 1;
            Debug.Log("Enemy Injured");
        }
        if (evilHealth == 0)
        {
            Destroy(gameObject);
        }
    } 

}
