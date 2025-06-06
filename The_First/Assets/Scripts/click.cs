using UnityEngine;

public class Bird : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }

    void Update()
    {
        if (!GameManager.Instance.isGameStarted || isDead) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            GameManager.Instance.PlaySound(GameManager.Instance.wingSound);
        }
    }

    public void StartFlying()
    {
        rb.simulated = true;
        Jump(); // Bay 1 phát đầu tiên
    }

    void Jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        isDead = true;
        GameManager.Instance.GameOver();
    }
}
