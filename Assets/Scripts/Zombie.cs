using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieData zombieData;

    [Header("���ǵ�")]
    public float speed = 5f; // �̵� �ӵ�

    [Header("�浹 ����")]
    public bool truckHit = false;
    public bool block = false;

    [Header("���� ����")]
    public float jumpForce = 5f; // ���� ��
    public float jumpDuration = 0.5f; // ���� ���� �ð�
    private bool isJumping = false; // ���� ���� ����
    public float jumpTimer = 0f;    // ���� Ÿ�̸�
    public float jumpDelay = 2f;    // ���� ������

    Status targetBox;        // Ÿ���ϰ� �ִ� �ڽ�
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
        Move(); // �̵� �Լ� ȣ��
    }

    void Move()
    {
        // -1 �������� �̵�
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
        isJumping = true; // ���� �� ���� ����
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // ���� �� ����

        // ���� ���� �ð� ���� ���
        yield return new WaitForSeconds(jumpDuration);

        // ���� �� �߰� ������ �ʿ��� ��� ���⿡ �ۼ�
        isJumping = false; // ���� �Ϸ� ���·� ����
    }


    public void Attack(bool b, Status box = null)
    {
        targetBox = box;
        animator.SetBool("IsAttacking", b);
    }

    /// <summary>
    /// ���� ���� Function
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

    // �ִϸ��̼��� ��� ������ Ȯ���ϴ� �޼���
    private bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0�� �⺻ ���̾�
        return stateInfo.IsName(animationName); // �ִϸ��̼� �̸��� ��ġ�ϴ��� Ȯ��
    }
}
