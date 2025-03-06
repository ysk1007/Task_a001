using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("���� ����Ʈ")]
    public Transform bulletPoint;

    [Header("���� ����")]
    public float attackTimer;   // ���� Ÿ�̸�
    public float attackSpeed;   // ���� �ӵ�

    void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer >= attackSpeed)
        {
            attackTimer = 0;
            Shoot();
        }
    }

    /// <summary>
    /// �Ѿ� �߻�
    /// </summary>
    public void Shoot()
    {
        ObjectPool.Instance.GetFromPool("Bullet");
    }
}
