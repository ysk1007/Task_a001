using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("히어로 반동 애니메이션")]
    public Animator heroAnimator;

    [Header("공격 포인트")]
    public Transform bulletPoint;

    [Header("공격 관련")]
    public float attackTimer;   // 공격 타이머
    public float attackSpeed;   // 공격 속도

    [Header("샷건 옵션")]
    public int bulletCount = 5; // 한 번에 발사할 총알 개수
    public float spreadAngle = 30f; // 퍼지는 각도 (최대 각도)

    [Header("총알 데이터")]
    public BulletData bulletData;

    [Header("총구 플래시")]
    public GameObject muzzleFlash;

    private void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        // 히어로 공격 속도에 맞춰서 모션 속도 설정
        heroAnimator.SetFloat("MotionSpeed", 1 / attackSpeed);
    }

    void Update()
    {
        SetUp();

        // 공격 속도에 맞에 공격
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackSpeed)
        {
            attackTimer = 0;
            Shoot();
        }
    }

    /// <summary>
    /// 샷건처럼 여러 발의 총알을 퍼트리며 발사
    /// </summary>
    public void Shoot()
    {
        // 총구 플래시
        StartCoroutine("Flash");

        for (int i = 0; i < bulletCount; i++)
        {
            // 랜덤한 퍼짐 각도 계산
            float angleOffset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

            // 현재 총구 방향에서 회전 적용
            Quaternion bulletRotation = bulletPoint.rotation * Quaternion.Euler(0, 0, angleOffset);

            // 오브젝트 풀에서 총알 가져와서 회전 적용
            GameObject bullet = ObjectPool.Instance.GetFromPool(bulletData.BulletTag, bulletPoint);
            bullet.GetComponent<Bullet>().SetUp(bulletData);
            bullet.transform.rotation = bulletRotation;
        }

        // 히어로 반동 애니메이션
        heroAnimator.SetTrigger("Shoot");
    }

    // 총구 플래시 재생
    IEnumerator Flash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.08f);
        muzzleFlash.SetActive(false);
    }

}
