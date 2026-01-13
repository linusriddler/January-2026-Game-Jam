using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpStrength = 5f;
    public Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private bool isGrounded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        float moveInputLR = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector3(moveInputLR * speed, rb.linearVelocity.z);
        float moveInputFB = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = new Vector3(moveInputFB * speed, rb.linearVelocity.x);
    }
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
