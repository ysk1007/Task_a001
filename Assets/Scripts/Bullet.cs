using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    private float damage;           // �����
    private float bulletSpeed;      // �ӵ�
    private float bulletLifeTime;   // ���� �ð�
    private float penetration;      // �����

    private void Awake()
    {
        SetUp();
    }

    private void Update()
    {
        // �������� �̵�
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    /// <summary>
    /// �Ѿ� ������ ����
    /// </summary>
    void SetUp()
    {
        damage = bulletData.Damage;
        bulletSpeed = bulletData.MoveSpeed;
        bulletLifeTime = bulletData.LifeTime;
        penetration = bulletData.Penetration;
    }
}
