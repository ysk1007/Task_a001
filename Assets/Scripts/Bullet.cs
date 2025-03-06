using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    public TrailRenderer trailRenderer;

    private float damage;           // �����
    private float bulletSpeed;      // �ӵ�
    private float lifeTimer;        // ���� �ð� Ÿ�̸�
    private float bulletLifeTime;   // ���� �ð�
    private bool  bulletActive;     // �Ѿ� Ȱ��ȭ ����
    private float penetration;      // �����

    HashSet<Collision2D> hitZombies = new HashSet<Collision2D>();

    private void OnEnable()
    {
        SetUp(bulletData);
    }

    private void Update()
    {
        // �������� �̵�
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
    /// �Ѿ� ������ ����
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
            hitZombies.Add(collision); // �̹� ���� ���� ���
            collision.transform.GetComponent<Status>().TakeDamage(damage);
            ObjectPool.Instance.GetFromPool("DamageText", collision.transform).GetComponent<DamageText>().SetUp(damage);
            penetration--;
            // ������� 0�� �Ǹ� �Ѿ��� �ı�
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
