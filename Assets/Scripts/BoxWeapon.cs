using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWeapon : MonoBehaviour
{
    [Header("무기 데이터")]
    public WeaponData weaponData;       

    [Header("탐색 범위")]
    public float range;                             // 감지 범위

    [Header("공격 위치")]
    public Transform bulletSpawnPoint;              // 공격 위치

    [HideInInspector] public LayerMask targetLayer; // 감지할 대상의 레이어

    [HideInInspector] public Transform target;      // 현재 바라볼 타겟
    [HideInInspector] public float attackTimer;                      // 공격 타이머
    [HideInInspector] public float attackSpeed;     // 공격 속도

    [HideInInspector] public bool isShooting;       // 공격 유무

    protected virtual void SetUp()
    {
        if (weaponData == null) return;

        range = weaponData.Range;
        attackSpeed = weaponData.AttackSpeed;
    }

    protected virtual void Attack()
    {

    }

    public virtual void Skill()
    {

    }

    /// <summary>
    /// 범위 안에서 가장 가까운 타겟을 찾음
    /// </summary>
    protected virtual void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);
        float closestDistance = range;
        Transform closestTarget = null;

        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = hit.transform;
            }
        }

        target = closestTarget;
    }

    /// <summary>
    /// 타겟을 향해 Z 축 기준으로 회전
    /// </summary>
    protected virtual void RotateTowardsTarget()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    /// <summary>
    /// 기즈모로 범위 디버깅
    /// </summary>
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
