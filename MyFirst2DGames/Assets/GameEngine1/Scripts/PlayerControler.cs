using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5.0f;

    [Header("점프 설정")]
    public float jumpForce = 10.0f;

    [Header("플레이어 설정")]
    public int maxHealth = 1; // 간단한 목숨 설정 (1이면 한 번만 맞으면 죽음)

    private Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private int score = 0;
    private int currentHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
            Debug.LogError("Rigidbody2D가 없습니다!");

        currentHealth = maxHealth;
    }

    void Update()
    {
        // 좌우 이동
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("점프!");
        }

        // 애니메이션 (속도 기반)
        float currentSpeed = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", currentSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥 감지
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("바닥에 착지!");
        }

        // ✅ 장애물 감지
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("⚠️ 장애물에 충돌!");
            TakeDamage(1);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("바닥에서 떨어짐");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 코인 획득
        if (other.CompareTag("Coin"))
        {
            score++;
            Debug.Log("코인 획득! 현재 점수: " + score);
            Destroy(other.gameObject);
        }
    }

    // ✅ 데미지 처리 함수
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit"); // 피격 애니메이션 (Animator에 "Hit" Trigger 필요)

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ✅ 사망 처리
    void Die()
    {
        Debug.Log("💀 플레이어 사망!");
        animator.SetTrigger("Die"); // Animator에 "Die" Trigger 추가 가능
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // 물리 정지
        GetComponent<Collider2D>().enabled = false; // 충돌 비활성화
        this.enabled = false; // PlayerController 비활성화 (입력 막기)
    }
}
