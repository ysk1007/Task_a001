using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : BoxWeapon
{
    [Header("�ѱ� �÷���")]
    public GameObject muzzleFlash;

    [Header("��ų �ɼ�")]
    public int bulletCount = 6;         // �Ѿ� ����
    public float spreadAngle = 30f;     // ��ä�� ���� (��ü ����)

    private void Awake()
    {
        SetUp();

        // ������ ���̾� ����
        targetLayer = LayerMask.GetMask("front", "middle", "back");
    }

    private void Start()
    {
        // ������ �� ���� �Ŵ����� ���� ���
        GameManager.instance.weapons.Add(this);
    }

    void Update()
    {
        // Ÿ���� ������
        if (target == null) 
            FindTarget(); // Ÿ���� ã�ƾ� ��
        else
        {
            // Ÿ���� active�� off �Ǹ� return
            if (!target.gameObject.activeInHierarchy)
            {
                target = null;
                return;
            }

            // ���� �ӵ��� ���缭 ����
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                attackTimer = 0;

                Firing();
            }

            // Ÿ���� �ٶ󺸸鼭 ȸ��
            RotateTowardsTarget();
        }
    }

    /// <summary>
    /// 5�� ���� Attack() 5�� ����
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
    /// �Ѿ� �߻�
    /// </summary>
    protected override void Attack()
    {
        // �ѱ� �÷���
        StartCoroutine("Flash");

        // ������Ʈ Ǯ���� �Ѿ� ��������
        GameObject bullet = ObjectPool.Instance.GetFromPool(weaponData._BulletData.BulletTag, bulletSpawnPoint);
        
        // �Ѿ� �¾�
        bullet.GetComponent<Bullet>().SetUp(weaponData._BulletData);
    }

    /// <summary>
    /// ��ä�÷� �Ѿ� �߻�
    /// </summary>
    public override void Skill()
    {
        float startAngle = bulletSpawnPoint.rotation.eulerAngles.z - (spreadAngle / 2);
        float angleStep = spreadAngle / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + (angleStep * i);

            // ������Ʈ Ǯ���� �Ѿ� ��������
            GameObject bullet = ObjectPool.Instance.GetFromPool(weaponData._BulletData.BulletTag, bulletSpawnPoint);
            
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    /// <summary>
    /// �ѱ� �÷���
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
