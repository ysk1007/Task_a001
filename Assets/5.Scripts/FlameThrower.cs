using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : BoxWeapon
{
    [Space]
    [Header("파티클")]
    public ParticleSystem particle;         // 파티클 시스템

    [Header("스킬")]
    public bool isSkill = false;            // 스킬 사용 여부
    public float skillTimer = 0f;           // 스킬 타이머
    public float skillDuration = 3f;        // 스킬 지속 시간

    float defaultLifeTime = 0.8f;           // 파티클 기본 생명 시간
    float skiilLifeTime = 1.6f;             // 스킬 생명 시간

    [Header("피격 범위(collider) 관리")]
    public Vector2 defaultOffSet;           // 기본 피격 위치
    public Vector2 skillOffSet;             // 스킬 피격 위치

    public Vector2 defaultSize;             // 기본 피격 범위
    public Vector2 skillSize;               // 스킬 피격 범위

    bool isAttack = false;                  // 공격 여부

    BoxCollider2D collider;
    HashSet<Collider2D> hitZombies = new HashSet<Collider2D>(); // 피격한 좀비 체크 리스트
    private void Awake()
    {
        SetUp();
        collider = GetComponent<BoxCollider2D>();

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
        // 스킬 사용중이라면
        if (isSkill)
        {
            skillTimer += Time.deltaTime;

            if(skillTimer >= skillDuration) // 지속시간 끝나면 종료
            {
                SkillUpdate(false);
            }
        }

        // 타겟이 없을때
        if (target == null)
        { 
            FindTarget();       // 타겟을 찾아야 함
            Particle(false);    // 파티클 끄기
        }
        else
        {
            // 타겟이 active가 off 되면 return
            if (!target.gameObject.activeInHierarchy)
            {
                target = null;
                return;
            }

            Particle(true);

            // 공격 속도에 맞춰서 공격
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                attackTimer = 0;
                Attack();
            }

            // 타겟을 바라보면서 회전
            RotateTowardsTarget();
        }
    }

    protected override void Attack()
    {
        hitZombies.Clear();
    }

    public override void Skill()
    {
        SkillUpdate(true);
    }

    /// <summary>
    /// 스킬 on/off시 변수 설정
    /// </summary>
    /// <param name="b"></param>
    void SkillUpdate(bool b)
    {
        skillTimer = 0;
        isSkill = b;
        particle.startLifetime = b ? skiilLifeTime : defaultLifeTime;
        collider.size = b ? skillSize : defaultSize;
        collider.offset = b ? skillOffSet : defaultOffSet;
    }

    /// <summary>
    /// 파티클 on/off
    /// </summary>
    /// <param name="b"></param>
    void Particle(bool b)
    {
        particle.loop = b;
        isAttack = b;

        if(b)
            particle.Play();
        else
            particle.Stop();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAttack) return;

        // 좀비 태그를 가졌고 피격한적 없으면
        if (collision.gameObject.tag == "Zombie" && !hitZombies.Contains(collision))
        {
            hitZombies.Add(collision); // 이미 맞은 좀비를 기록

            // 대미지
            float damage = weaponData._BulletData.Damage;

            // 대미지 적용하고 텍스트 출력
            collision.transform.GetComponent<Status>().TakeDamage(damage);
            ObjectPool.Instance.GetFromPool("DamageText", collision.transform).GetComponent<DamageText>().SetUp(damage);
        }
    }

    protected override void SetUp() { base.SetUp(); }

    protected override void FindTarget() { base.FindTarget(); }

    protected override void RotateTowardsTarget() { base.RotateTowardsTarget(); }

    protected override void OnDrawGizmos() { base.OnDrawGizmos(); }
}
