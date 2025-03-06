using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : BoxWeapon
{
    private void Awake()
    {
        SetUp();
        targetLayer = LayerMask.GetMask("front", "middle", "back");
    }

    void Update()
    {
        if (target == null) FindTarget();
        else
        {
            if (!target.gameObject.activeInHierarchy)
            {
                target = null;
                return;
            }

            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                attackTimer = 0;

                float delay = 0;
                for (int i = 0; i < 5; i++)
                {
                    Invoke("Attack", delay);
                    delay += 0.1f;
                }
            }

            RotateTowardsTarget();
        }
    }

    protected override void Attack()
    {
        GameObject bullet = ObjectPool.Instance.GetFromPool("Bullet", bulletSpawnPoint);
        bullet.GetComponent<Bullet>().SetUp(weaponData._BulletData);
    }

    protected override void SetUp() {base.SetUp();}

    protected override void FindTarget() {base.FindTarget();}

    protected override void RotateTowardsTarget() {base.RotateTowardsTarget();}

    protected override void OnDrawGizmos() {base.OnDrawGizmos();}
}
