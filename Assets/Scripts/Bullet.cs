using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    public TrailRenderer trailRenderer;

    private float damage;           // 대미지
    private float bulletSpeed;      // 속도
    private float lifeTimer;        // 유지 시간 타이머
    private float bulletLifeTime;   // 유지 시간
    private bool  bulletActive;     // 총알 활성화 여부
    private float penetration;      // 관통력

    HashSet<Collision2D> hitZombies = new HashSet<Collision2D>();

    private void OnEnable()
    {
        SetUp(bulletData);
    }

    private void Update()
    {
        // 방향으로 이동
        transform.position += transform.up * bulletSpeed * Time.deltaTime;

        if (!bulletActive) return;

        lifeTimer += Time.deltaTime;
        if(lifeTimer >= bulletLifeTime)
        {
            lifeTimer = 0;
            ReturnToPool();
        }
    }

    /// <summary>
    /// 총알 데이터 설정
    /// </summary>
    public void SetUp(BulletData bulletData)
    {
        trailRenderer.Clear();
        hitZombies.Clear();

        damage = bulletData.Damage;
        bulletSpeed = bulletData.MoveSpeed;
        bulletLifeTime = bulletData.LifeTime;
        penetration = bulletData.Penetration;

        bulletActive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            ReturnToPool();
            return;
        }

        if (collision.gameObject.tag == "Zombie" && !hitZombies.Contains(collision))
        {
            hitZombies.Add(collision); // 이미 맞은 좀비를 기록
            collision.transform.GetComponent<Status>().TakeDamage(damage);
            ObjectPool.Instance.GetFromPool("DamageText", collision.transform).GetComponent<DamageText>().SetUp(damage);
            penetration--;
            // 관통력이 0이 되면 총알을 파괴
            if (penetration <= 0)
            {
                ReturnToPool();
            }
        }
    }

    void ReturnToPool()
    {
        bulletActive = false;
        ObjectPool.Instance.ReturnToPool("Bullet", gameObject);
    }
}
