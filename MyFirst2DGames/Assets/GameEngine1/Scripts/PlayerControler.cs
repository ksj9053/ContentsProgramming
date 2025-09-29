using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    // Animator 컴포넌트 참조
    private Animator animator;

    // 캐릭터 이동 여부 체크용 변수
    private bool isMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            Debug.Log("Animator 컴포넌트를 찾았습니다!");
        }
        else
        {
            Debug.LogError("Animator 컴포넌트가 없습니다!");
        }
    }

    void Update()
{
    Vector3 movement = Vector3.zero;
    isMoving = false;

    // --- 좌우 이동 ---
    if (Input.GetKey(KeyCode.A))
    {
        movement += Vector3.left;
        transform.localScale = new Vector3(-1, 1, 1);
        isMoving = true;
    }
    if (Input.GetKey(KeyCode.D))
    {
        movement += Vector3.right;
        transform.localScale = new Vector3(1, 1, 1);
        isMoving = true;
    }

    // --- 달리기 (Shift) ---
    float currentMoveSpeed = moveSpeed;
    if (Input.GetKey(KeyCode.LeftShift) && isMoving)
    {
        currentMoveSpeed = moveSpeed * 2f;
        Debug.Log("달리기 모드!");
    }

    // --- 이동 적용 ---
    if (movement != Vector3.zero)
    {
        transform.Translate(movement * currentMoveSpeed * Time.deltaTime);
    }

    // --- 점프 입력 ---
    if (Input.GetKeyDown(KeyCode.Space))
    {
        if (animator != null)
        {
            animator.SetBool("isJumping", true);
            Debug.Log("점프!");
        }
    }

    // --- 점프 종료 감지 (방법 B) ---
    if (animator != null)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 1.0f)
        {
            animator.SetBool("isJumping", false);
        }

        // Speed 파라미터 갱신
        float currentSpeed = isMoving ? currentMoveSpeed : 0f;
        animator.SetFloat("Speed", currentSpeed);
    }
}
}