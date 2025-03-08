using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Zombie : MonoBehaviour
{
    [Header("���� ������")]
    public ZombieData zombieData;

    [Header("���ǵ�")]
    public float speed = 5f;        // �̵� �ӵ�

    [Header("�浹 ����")]
    public bool truckHit = false;   // Ʈ���� ��������
    public bool block = false;      // �� �տ� ����� ���� �������� Ȯ��

    [Header("���� ����")]
    public float jumpForce = 5f;        // ���� ��
    public float jumpDuration = 0.5f;   // ���� ���� �ð�
    private bool isJumping = false;     // ���� ���� ����
    public float jumpTimer = 0f;        // ���� Ÿ�̸�
    public float jumpDelay = 2f;        // ���� ������

    public float moveDuration = 0.5f; // Z�� �̵� �ð�
    private bool isMoving = false;
    Status targetBox;                   // Ÿ���ϰ� �ִ� �ڽ�
    Status status;                      // �ɷ�ġ
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
        // ���񿡰� ���� ����
        if (block && !isJumping)
        {
            // ���� Ÿ�̸ӿ� + �ð�
            jumpTimer += Time.deltaTime;

            // ������ �ð� �������� ������ ��������
            if(jumpTimer >= jumpDelay) Jumpping();
        }
        else
        {
            jumpTimer = 0f;
        }

        Move(); // �̵� �Լ� ȣ��
    }

    void Move()
    {
        // -1 �������� �̵�
        // ���� ���� ���̸� �ڷ� ���� �̵�
        rb.velocity = new Vector2( IsAnimationPlaying("Attack") ? speed / 4 : -speed, rb.velocity.y);
    }

    // ����
    void Jumpping()
    {
        jumpTimer = 0f;

        // �������̸� ���
        if (isJumping) return;

        isJumping = true; // ���� �� ���� ����
        // ���� ���
        StartCoroutine(Jump());
    }

    /// <summary>
    /// ���� ��� �ڷ�ƾ
    /// </summary>
    private IEnumerator Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // ���� �� ����

        // ���� ���� �ð� ���� ���
        yield return new WaitForSeconds(jumpDuration);

        isJumping = false; // ���� �Ϸ�
    }

    // Ÿ���ڽ� ����
    public void Attack(bool b, Status box = null)
    {
        targetBox = box;

        // ���� �ִϸ��̼� ����
        animator.SetBool("IsAttacking", b);
    }

    /// <summary>
    /// ���� ���� Function
    /// </summary>
    public void OnAttack()
    {
        // ���̾� ����
        layerChange();

        // Ÿ���ϴ� �ڽ�Ÿ���� ���ݷ� ��ŭ �����
        targetBox?.TakeDamage(status.attackDamage);
    }

    // ���� ����
    public void EndAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    /// <summary>
    /// 2D���� ��ü������ ��ȯ�ϴ� ����� �����ַ���
    /// ���̾�� Z���� �ٲٸ鼭 ���ٰ��� ������
    /// </summary>
    void layerChange()
    {
        // ���� ���̾ �����ͼ� ���� ���̰��� ���� ������
        int layerIndex = gameObject.layer + 1;

        // ���� ���̾ �ִ� 8�� ���� ������ ó�� 6���� ����
        // ���� �������� ���� ���̾� ��
        gameObject.layer = layerIndex > 8 ? 6 : layerIndex;

        // ���̾� ���� Z�� �ٸ��� ����
        Vector3 pos = transform.position;

        // ��ǥ Z�� ����
        float targetZ = gameObject.layer switch
        {
            6 => -1f,  // Front
            7 => 0f,  // Middle
            8 => 1f,  // Back
            _ => transform.position.z
        };

        // �ε巯�� �̵� ����
        StartCoroutine(SmoothMoveZ(targetZ, moveDuration));
    }

    // �ִϸ��̼��� ��� ������ Ȯ���ϴ� �޼���
    private bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0�� �⺻ ���̾�
        return stateInfo.IsName(animationName); // �ִϸ��̼� �̸��� ��ġ�ϴ��� Ȯ��
    }

    /// <summary>
    /// Z�� �ε巴�� �����̴� �ڷ�ƾ
    /// </summary>
    IEnumerator SmoothMoveZ(float targetZ, float duration)
    {
        isMoving = true;

        // ���� ��ġ
        Vector3 startPos = transform.position;

        // ���� ��ġ
        Vector3 endPos = new Vector3(startPos.x, startPos.y, targetZ);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Lerp�� ����Ͽ� �ε巴�� Z�� �̵�
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� ��ġ ����
        transform.position = endPos;

        isMoving = false;
    }
}
