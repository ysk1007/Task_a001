using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : BoxWeapon
{
    [Space]
    [Header("파티클")]
    public ParticleSystem particle;

    bool isAttack = false;
    HashSet<Collider2D> hitZombies = new HashSet<Collider2D>();
    private void Awake()
    {
        SetUp();
        targetLayer = LayerMask.GetMask("front", "middle", "back");
    }

    void Update()
    {
        if (target == null)
        { 
            FindTarget();
            Particle(false);
        }
        else
        {
            if (!target.gameObject.activeInHierarchy)
            {
                target = null;
                return;
            }
            Particle(true);
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                attackTimer = 0;
                Attack();
            }

            RotateTowardsTarget();
        }
    }

    protected override void Attack()
    {
        hitZombies.Clear();
    }

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

        if (collision.gameObject.tag == "Zombie" && !hitZombies.Contains(collision))
        {
            hitZombies.Add(collision); // 이미 맞은 좀비를 기록

            float damage = weaponData._BulletData.Damage;

            collision.transform.GetComponent<Status>().TakeDamage(damage);
            ObjectPool.Instance.GetFromPool("DamageText", collision.transform).GetComponent<DamageText>().SetUp(damage);
        }
    }

    protected override void SetUp() { base.SetUp(); }

    protected override void FindTarget() { base.FindTarget(); }

    protected override void RotateTowardsTarget() { base.RotateTowardsTarget(); }

    protected override void OnDrawGizmos() { base.OnDrawGizmos(); }
}
