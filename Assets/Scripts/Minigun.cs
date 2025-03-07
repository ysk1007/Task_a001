using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : BoxWeapon
{
    [Header("ÃÑ±¸ ÇÃ·¡½Ã")]
    public GameObject muzzleFlash;

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

                Firing();
            }

            RotateTowardsTarget();
        }
    }

    void Firing()
    {
        float delay = 0;
        for (int i = 0; i < 5; i++)
        {
            Invoke("Attack", delay);
            delay += 0.2f;
        }
    }

    protected override void Attack()
    {
        StartCoroutine("Flash");
        GameObject bullet = ObjectPool.Instance.GetFromPool(weaponData._BulletData.BulletTag, bulletSpawnPoint);
        bullet.GetComponent<Bullet>().SetUp(weaponData._BulletData);
    }

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
