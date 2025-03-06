using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Animator heroAnimator;

    [Header("���� ����Ʈ")]
    public Transform bulletPoint;

    [Header("���� ����")]
    public float attackTimer;   // ���� Ÿ�̸�
    public float attackSpeed;   // ���� �ӵ�

    [Header("���� �ɼ�")]
    public int bulletCount = 5; // �� ���� �߻��� �Ѿ� ����
    public float spreadAngle = 30f; // ������ ���� (�ִ� ����)

    [Header("�Ѿ� ������")]
    public BulletData bulletData;

    private void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        heroAnimator.SetFloat("MotionSpeed", 1 / attackSpeed);
    }

    void Update()
    {
        SetUp();

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackSpeed)
        {
            attackTimer = 0;
            Shoot();
        }
    }

    /// <summary>
    /// ����ó�� ���� ���� �Ѿ��� ��Ʈ���� �߻�
    /// </summary>
    public void Shoot()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            // ������ ���� ���� ��� (-spreadAngle/2 ~ +spreadAngle/2)
            float angleOffset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

            // ���� �ѱ� ���⿡�� ȸ�� ����
            Quaternion bulletRotation = bulletPoint.rotation * Quaternion.Euler(0, 0, angleOffset);

            // ������Ʈ Ǯ���� �Ѿ� �����ͼ� ȸ�� ����
            GameObject bullet = ObjectPool.Instance.GetFromPool("Bullet", bulletPoint);
            bullet.GetComponent<Bullet>().SetUp(bulletData);
            bullet.transform.rotation = bulletRotation;
        }
        heroAnimator.SetTrigger("Shoot");
    }
}
