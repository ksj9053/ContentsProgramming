using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("캐릭터 설정")]
    public string playerName = "김성준"; 
    public float moveSpeed = 6.5f;         
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isMoving = false;
    private bool isJumpingNow = false;

    void Start()
    {
        // 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 캐릭터 소개
        Debug.Log("안녕하세요, " + playerName + "님!");
        Debug.Log("이동 속도: " + moveSpeed);
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

        // --- 점프 ---
        if (Input.GetKeyDown(KeyCode.Space) && !isJumpingNow)
        {
            isJumpingNow = true;
            animator.SetBool("isJumping", true);
            Debug.Log(playerName + "님이 점프했습니다!");
        }

        // --- 점프 종료 감지 ---
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 1.0f)
        {
            animator.SetBool("isJumping", false);
            isJumpingNow = false;
        }

        // --- 애니메이터 Speed 파라미터 갱신 ---
        float currentSpeed = isMoving ? currentMoveSpeed : 0f;
        animator.SetFloat("Speed", currentSpeed);
    }
}
