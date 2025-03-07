using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("����� �ݵ� �ִϸ��̼�")]
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

    [Header("�ѱ� �÷���")]
    public GameObject muzzleFlash;

    private void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        // ����� ���� �ӵ��� ���缭 ��� �ӵ� ����
        heroAnimator.SetFloat("MotionSpeed", 1 / attackSpeed);
    }

    void Update()
    {
        SetUp();

        // ���� �ӵ��� �¿� ����
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
        // �ѱ� �÷���
        StartCoroutine("Flash");

        for (int i = 0; i < bulletCount; i++)
        {
            // ������ ���� ���� ���
            float angleOffset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

            // ���� �ѱ� ���⿡�� ȸ�� ����
            Quaternion bulletRotation = bulletPoint.rotation * Quaternion.Euler(0, 0, angleOffset);

            // ������Ʈ Ǯ���� �Ѿ� �����ͼ� ȸ�� ����
            GameObject bullet = ObjectPool.Instance.GetFromPool(bulletData.BulletTag, bulletPoint);
            bullet.GetComponent<Bullet>().SetUp(bulletData);
            bullet.transform.rotation = bulletRotation;
        }

        // ����� �ݵ� �ִϸ��̼�
        heroAnimator.SetTrigger("Shoot");
    }

    // �ѱ� �÷��� ���
    IEnumerator Flash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.08f);
        muzzleFlash.SetActive(false);
    }

}
