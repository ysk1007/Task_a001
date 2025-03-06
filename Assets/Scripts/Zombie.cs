using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieData zombieData;

    [Header("스피드")]
    public float speed = 5f; // 이동 속도

    [Header("충돌 감지")]
    public bool truckHit = false;
    public bool block = false;

    [Header("점프 관련")]
    public float jumpForce = 5f; // 점프 힘
    public float jumpDuration = 0.5f; // 점프 지속 시간
    private bool isJumping = false; // 현재 점프 유무
    public float jumpTimer = 0f;    // 점프 타이머
    public float jumpDelay = 2f;    // 점프 딜레이

    Status targetBox;        // 타겟하고 있는 박스
    Status status;
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
        if (block)
        {
            jumpTimer += Time.deltaTime;
            if(jumpTimer >= jumpDelay) Jumpping();
        }
        else
        {
            jumpTimer = 0f;
        }

        //if (truckHit) return;
        Move(); // 이동 함수 호출
    }

    void Move()
    {
        // -1 방향으로 이동
        rb.velocity = new Vector2( IsAnimationPlaying("Attack") ? speed / 4 : -speed, rb.velocity.y);
    }

    void Jumpping()
    {
        if (isJumping) return;
        jumpTimer = 0f;
        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        isJumping = true; // 점프 중 상태 설정
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 점프 힘 적용

        // 점프 지속 시간 동안 대기
        yield return new WaitForSeconds(jumpDuration);

        // 점프 후 추가 로직이 필요한 경우 여기에 작성
        isJumping = false; // 점프 완료 상태로 변경
    }


    public void Attack(bool b, Status box = null)
    {
        targetBox = box;
        animator.SetBool("IsAttacking", b);
    }

    /// <summary>
    /// 실제 공격 Function
    /// </summary>
    public void OnAttack()
    {
        layerChange();
        targetBox?.TakeDamage(status.attackDamage);
    }

    public void EndAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    void layerChange()
    {
        int layerIndex = gameObject.layer + 1;
        gameObject.layer = layerIndex > 8 ? 6 : layerIndex;

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
