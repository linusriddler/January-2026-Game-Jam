using UnityEngine;

public class Bat : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 offset = new Vector3(0.4f, -0.3f, 0.6f);
    public float smoothSpeed = 10f;

    public float spinSpeed = 720f;
    public float attackTime = 0.25f;
    public bool batCooldown = false;

    private float attackTimer;
    private float spinAngle;

    void Update()
    {
        // FPS input
        if (Input.GetMouseButtonDown(0) && !batCooldown)
        {
            batCooldown = true;
            attackTimer = attackTime;
        }
    }

    void LateUpdate()
    {
        // Follow camera position
        Vector3 targetPosition =
            cameraTransform.position +
            cameraTransform.right * offset.x +
            cameraTransform.up * offset.y +
            cameraTransform.forward * offset.z;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        // Handle spinning
        if (batCooldown)
        {
            spinAngle += spinSpeed * Time.deltaTime;
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                batCooldown = false;
                spinAngle = 0f;
            }
        }

        // Camera rotation + spin
        transform.rotation =
            cameraTransform.rotation *
            Quaternion.Euler(spinAngle, 0f, 0f);
    }
}