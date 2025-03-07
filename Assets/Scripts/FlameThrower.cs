using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : BoxWeapon
{
    [Space]
    [Header("��ƼŬ")]
    public ParticleSystem particle;         // ��ƼŬ �ý���

    [Header("��ų")]
    public bool isSkill = false;            // ��ų ��� ����
    public float skillTimer = 0f;           // ��ų Ÿ�̸�
    public float skillDuration = 3f;        // ��ų ���� �ð�

    float defaultLifeTime = 0.8f;           // ��ƼŬ �⺻ ���� �ð�
    float skiilLifeTime = 1.6f;             // ��ų ���� �ð�

    [Header("�ǰ� ����(collider) ����")]
    public Vector2 defaultOffSet;           // �⺻ �ǰ� ��ġ
    public Vector2 skillOffSet;             // ��ų �ǰ� ��ġ

    public Vector2 defaultSize;             // �⺻ �ǰ� ����
    public Vector2 skillSize;               // ��ų �ǰ� ����

    bool isAttack = false;                  // ���� ����

    BoxCollider2D collider;
    HashSet<Collider2D> hitZombies = new HashSet<Collider2D>(); // �ǰ��� ���� üũ ����Ʈ
    private void Awake()
    {
        SetUp();
        collider = GetComponent<BoxCollider2D>();

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
        // ��ų ������̶��
        if (isSkill)
        {
            skillTimer += Time.deltaTime;

            if(skillTimer >= skillDuration) // ���ӽð� ������ ����
            {
                SkillUpdate(false);
            }
        }

        // Ÿ���� ������
        if (target == null)
        { 
            FindTarget();       // Ÿ���� ã�ƾ� ��
            Particle(false);    // ��ƼŬ ����
        }
        else
        {
            // Ÿ���� active�� off �Ǹ� return
            if (!target.gameObject.activeInHierarchy)
            {
                target = null;
                return;
            }

            Particle(true);

            // ���� �ӵ��� ���缭 ����
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                attackTimer = 0;
                Attack();
            }

            // Ÿ���� �ٶ󺸸鼭 ȸ��
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
    /// ��ų on/off�� ���� ����
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
    /// ��ƼŬ on/off
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

        // ���� �±׸� ������ �ǰ����� ������
        if (collision.gameObject.tag == "Zombie" && !hitZombies.Contains(collision))
        {
            hitZombies.Add(collision); // �̹� ���� ���� ���

            // �����
            float damage = weaponData._BulletData.Damage;

            // ����� �����ϰ� �ؽ�Ʈ ���
            collision.transform.GetComponent<Status>().TakeDamage(damage);
            ObjectPool.Instance.GetFromPool("DamageText", collision.transform).GetComponent<DamageText>().SetUp(damage);
        }
    }

    protected override void SetUp() { base.SetUp(); }

    protected override void FindTarget() { base.FindTarget(); }

    protected override void RotateTowardsTarget() { base.RotateTowardsTarget(); }

    protected override void OnDrawGizmos() { base.OnDrawGizmos(); }
}
