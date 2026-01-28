using UnityEngine;
using TMPro;

public class Beans : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float floatHeight = 0.5f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Smooth up-and-down floating
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.AddCollectible();
            Destroy(gameObject);
        }
    }
}