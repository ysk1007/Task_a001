using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("좀비 데이터")]
    public ZombieData zombieData;

    [Header("스피드")]
    public float speed = 5f;        // 이동 속도

    [Header("충돌 감지")]
    public bool truckHit = false;   // 트럭에 막혔는지
    public bool block = false;      // 내 앞에 좀비로 길이 막혔는지 확인

    [Header("점프 관련")]
    public float jumpForce = 5f;        // 점프 힘
    public float jumpDuration = 0.5f;   // 점프 지속 시간
    private bool isJumping = false;     // 현재 점프 유무
    public float jumpTimer = 0f;        // 점프 타이머
    public float jumpDelay = 2f;        // 점프 딜레이

    Status targetBox;                   // 타겟하고 있는 박스
    Status status;                      // 능력치
    Rigidbody2D rb;
    Animator animator;

    private void OnEnable()
    {
        SetUp();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        status = GetComponent<Status>();
    }

    void SetUp()
    {
        speed = zombieData.MoveSpeed;
        status.SetUp(zombieData);
    }

    void Update()
    {
        // 좀비에게 가로 막힘
        if (block)
        {
            // 점프 타이머에 + 시간
            jumpTimer += Time.deltaTime;

            // 정해진 시간 지났으면 점프로 지나가기
            if(jumpTimer >= jumpDelay) Jumpping();
        }
        else
        {
            jumpTimer = 0f;
        }

        Move(); // 이동 함수 호출
    }

    void Move()
    {
        // -1 방향으로 이동
        // 만약 공격 중이면 뒤로 조금 이동
        rb.velocity = new Vector2( IsAnimationPlaying("Attack") ? speed / 4 : -speed, rb.velocity.y);
    }

    // 점프
    void Jumpping()
    {
        jumpTimer = 0f;

        // 점프중이면 취소
        if (isJumping) return;

        // 실제 기능
        StartCoroutine(Jump());
    }

    /// <summary>
    /// 점프 기능 코루틴
    /// </summary>
    private IEnumerator Jump()
    {
        isJumping = true; // 점프 중 상태 설정
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 점프 힘 적용

        // 점프 지속 시간 동안 대기
        yield return new WaitForSeconds(jumpDuration);

        isJumping = false; // 점프 완료
    }

    // 타워박스 공격
    public void Attack(bool b, Status box = null)
    {
        targetBox = box;

        // 공격 애니메이션 실행
        animator.SetBool("IsAttacking", b);
    }

    /// <summary>
    /// 실제 공격 Function
    /// </summary>
    public void OnAttack()
    {
        // 레이어 변경
        layerChange();

        // 타겟하는 박스타워에 공격력 만큼 대미지
        targetBox?.TakeDamage(status.attackDamage);
    }

    // 공격 종료
    public void EndAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    /// <summary>
    /// 2D에서 입체적으로 순환하는 모습을 보여주려고
    /// 레이어와 Z축을 바꾸면서 원근감을 보여줌
    /// </summary>
    void layerChange()
    {
        // 현재 레이어를 가져와서 다음 레이가될 값을 가져옴
        int layerIndex = gameObject.layer + 1;

        // 만약 레이어가 최대 8을 오버 했으면 처음 6으로 설정
        // 오버 안했으면 다음 레이어 값
        gameObject.layer = layerIndex > 8 ? 6 : layerIndex;

        // 레이어 별로 Z값 다르게 적용
        Vector3 pos = transform.position;
        switch (gameObject.layer)
        {
            // Front
            case 6:
                transform.position = new Vector3(pos.x, pos.y, -1);
                break;

            // Middle
            case 7:
                transform.position = new Vector3(pos.x, pos.y, 0);
                break;

            // Back
            case 8:
                transform.position = new Vector3(pos.x, pos.y, 1);
                break;
        }
    }

    // 애니메이션이 재생 중인지 확인하는 메서드
    private bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0은 기본 레이어
        return stateInfo.IsName(animationName); // 애니메이션 이름이 일치하는지 확인
    }
}
