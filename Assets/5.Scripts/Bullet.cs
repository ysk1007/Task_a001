using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("�Ѿ� ������")]
    public BulletData bulletData;

    [Header("�Ѿ� Ʈ����")]
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

        // ������ Ÿ���� ������
        /*lifeTimer += Time.deltaTime;
        if(lifeTimer >= bulletLifeTime)
        {
            lifeTimer = 0;

            // ������Ʈ Ǯ�� �����ֱ�
            ReturnToPool();
        }*/
    }

    /// <summary>
    /// �Ѿ� ������ ����
    /// </summary>
    public void SetUp(BulletData bulletData)
    {
        // �ǰ��� ���� ����Ʈ�� Ʈ���� �ʱ�ȭ
        trailRenderer.Clear();
        hitZombies.Clear();

        // �Ѿ� �ɷ�ġ ����
        damage = bulletData.Damage;
        bulletSpeed = bulletData.MoveSpeed;
        bulletLifeTime = bulletData.LifeTime;
        penetration = bulletData.Penetration;

        bulletActive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �� �±׸� ���� ������Ʈ�� �浹
        if (collision.gameObject.tag == "Wall")
        {
            // ������Ʈ Ǯ�� �����ֱ�
            ReturnToPool();
            return;
        }
        
        // �ǰ� ����Ʈ�� ����, ���� �±׸� ���� ������Ʈ�� �浹
        if (collision.gameObject.tag == "Zombie" && !hitZombies.Contains(collision))
        {
            hitZombies.Add(collision); // �̹� ���� ���� ���
            
            // ������ְ� �ؽ�Ʈ ǥ��
            collision.transform.GetComponent<Status>().TakeDamage(damage);
            ObjectPool.Instance.GetFromPool("DamageText", collision.transform).GetComponent<DamageText>().SetUp(damage);
            
            // ����� -1
            penetration--;

            // ������� 0�� �Ǹ� �Ѿ��� �ı�
            if (penetration <= 0)
            {
                // ������Ʈ Ǯ�� �����ֱ�
                ReturnToPool();
            }
        }
    }


    /// <summary>
    /// ������Ʈ Ǯ�� �����ִ� �Լ�
    /// </summary>
    void ReturnToPool()
    {
        if (!bulletActive) return;

        bulletActive = false;
        ObjectPool.Instance.ReturnToPool(bulletData.BulletTag, gameObject);
    }
}
