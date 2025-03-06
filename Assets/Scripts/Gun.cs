using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("공격 포인트")]
    public Transform bulletPoint;

    [Header("공격 관련")]
    public float attackTimer;   // 공격 타이머
    public float attackSpeed;   // 공격 속도

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
    /// 총알 발사
    /// </summary>
    public void Shoot()
    {
        ObjectPool.Instance.GetFromPool("Bullet");
    }
}
