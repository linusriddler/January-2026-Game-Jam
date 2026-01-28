using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform player;
    public float spawnInterval = 3f;

    public float minX = -30f;
    public float maxX = 54f;
    public float minZ = 47f;
    public float maxZ = 56f;

    public float spawnY = 0f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnZombie), spawnInterval, spawnInterval);
    }

    void SpawnZombie()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);

        GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

        // 🔥 THIS IS THE FIX
        EvilGuy evilGuy = zombie.GetComponent<EvilGuy>();
        if (evilGuy != null)
        {
            evilGuy.player = player;
        }
    }
}