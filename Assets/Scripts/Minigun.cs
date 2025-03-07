using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : BoxWeapon
{
    [Header("총구 플래시")]
    public GameObject muzzleFlash;

    [Header("스킬 옵션")]
    public int bulletCount = 6;         // 총알 개수
    public float spreadAngle = 30f;     // 부채꼴 범위 (전체 각도)

    private void Awake()
    {
        SetUp();

        // 감지할 레이어 설정
        targetLayer = LayerMask.GetMask("front", "middle", "back");
    }

    private void Start()
    {
        // 시작할 때 게임 매니저에 무기 등록
        GameManager.instance.weapons.Add(this);
    }

    void Update()
    {
        // 타겟이 없을때
        if (target == null) 
            FindTarget(); // 타겟을 찾아야 함
        else
        {
            // 타겟이 active가 off 되면 return
            if (!target.gameObject.activeInHierarchy)
            {
                target = null;
                return;
            }

            // 공격 속도에 맞춰서 공격
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                attackTimer = 0;

                Firing();
            }

            // 타겟을 바라보면서 회전
            RotateTowardsTarget();
        }
    }

    /// <summary>
    /// 5발 연사 Attack() 5번 실행
    /// </summary>
    void Firing()
    {
        float delay = 0;
        for (int i = 0; i < 5; i++)
        {
            Invoke("Attack", delay);
            delay += 0.2f;
        }
    }

    /// <summary>
    /// 총알 발사
    /// </summary>
    protected override void Attack()
    {
        // 총구 플래시
        StartCoroutine("Flash");

        // 오브젝트 풀에서 총알 가져오기
        GameObject bullet = ObjectPool.Instance.GetFromPool(weaponData._BulletData.BulletTag, bulletSpawnPoint);
        
        // 총알 셋업
        bullet.GetComponent<Bullet>().SetUp(weaponData._BulletData);
    }

    /// <summary>
    /// 부채꼴로 총알 발사
    /// </summary>
    public override void Skill()
    {
        float startAngle = bulletSpawnPoint.rotation.eulerAngles.z - (spreadAngle / 2);
        float angleStep = spreadAngle / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + (angleStep * i);

            // 오브젝트 풀에서 총알 가져오기
            GameObject bullet = ObjectPool.Instance.GetFromPool(weaponData._BulletData.BulletTag, bulletSpawnPoint);
            
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    /// <summary>
    /// 총구 플래시
    /// </summary>
    IEnumerator Flash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.08f);
        muzzleFlash.SetActive(false);
    }

    protected override void SetUp() {base.SetUp();}

    protected override void FindTarget() {base.FindTarget();}

    protected override void RotateTowardsTarget() {base.RotateTowardsTarget();}

    protected override void OnDrawGizmos() {base.OnDrawGizmos();}
}
