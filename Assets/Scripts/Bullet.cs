using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    private float damage;           // 대미지
    private float bulletSpeed;      // 속도
    private float bulletLifeTime;   // 유지 시간
    private float penetration;      // 관통력

    private void Awake()
    {
        SetUp();
    }

    private void Update()
    {
        // 방향으로 이동
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 총알 데이터 설정
    /// </summary>
    void SetUp()
    {
        damage = bulletData.Damage;
        bulletSpeed = bulletData.MoveSpeed;
        bulletLifeTime = bulletData.LifeTime;
        penetration = bulletData.Penetration;
    }
}
