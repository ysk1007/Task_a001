using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWeapon : MonoBehaviour
{
    public WeaponData weaponData;

    [Header("Ž�� ����")]
    public float range;     // ���� ����

    [Header("���� ��ġ")]
    public Transform bulletSpawnPoint; // ���� ��ġ

    [HideInInspector] public LayerMask targetLayer; // ������ ����� ���̾�

    [HideInInspector] public Transform target; // ���� �ٶ� Ÿ��
     public float attackTimer;  // ���� Ÿ�̸�
    [HideInInspector] public float attackSpeed;

    [HideInInspector] public bool isShooting;

    protected virtual void SetUp()
    {
        if (weaponData == null) return;
        range = weaponData.Range;
        attackSpeed = weaponData.AttackSpeed;
    }

    protected virtual void Attack()
    {

    }

    /// <summary>
    /// ���� �ȿ��� ���� ����� Ÿ���� ã��
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
    /// Ÿ���� ���� Z �� �������� ȸ��
    /// </summary>
    protected virtual void RotateTowardsTarget()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    /// <summary>
    /// ����� ����Ͽ� Ž�� ������ �ð������� ǥ��
    /// </summary>
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
