using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("총알 데이터")]
    public BulletData bulletData;

    [Header("총알 트레일")]
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

        // 라이프 타임이 지나면
        /*lifeTimer += Time.deltaTime;
        if(lifeTimer >= bulletLifeTime)
        {
            lifeTimer = 0;

            // 오브젝트 풀에 돌려주기
            ReturnToPool();
        }*/
    }

    /// <summary>
    /// 총알 데이터 설정
    /// </summary>
    public void SetUp(BulletData bulletData)
    {
        // 피격한 좀비 리스트와 트레일 초기화
        trailRenderer.Clear();
        hitZombies.Clear();

        // 총알 능력치 셋팅
        damage = bulletData.Damage;
        bulletSpeed = bulletData.MoveSpeed;
        bulletLifeTime = bulletData.LifeTime;
        penetration = bulletData.Penetration;

        bulletActive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 벽 태그를 가진 오브젝트와 충돌
        if (collision.gameObject.tag == "Wall")
        {
            // 오브젝트 풀에 돌려주기
            ReturnToPool();
            return;
        }
        
        // 피격 리스트에 없는, 좀비 태그를 가진 오브젝트와 충돌
        if (collision.gameObject.tag == "Zombie" && !hitZombies.Contains(collision))
        {
            hitZombies.Add(collision); // 이미 맞은 좀비를 기록
            
            // 대미지주고 텍스트 표시
            collision.transform.GetComponent<Status>().TakeDamage(damage);
            ObjectPool.Instance.GetFromPool("DamageText", collision.transform).GetComponent<DamageText>().SetUp(damage);
            
            // 관통력 -1
            penetration--;

            // 관통력이 0이 되면 총알을 파괴
            if (penetration <= 0)
            {
                // 오브젝트 풀에 돌려주기
                ReturnToPool();
            }
        }
    }


    /// <summary>
    /// 오브젝트 풀에 돌려주는 함수
    /// </summary>
    void ReturnToPool()
    {
        if (!bulletActive) return;

        bulletActive = false;
        ObjectPool.Instance.ReturnToPool(bulletData.BulletTag, gameObject);
    }
}
