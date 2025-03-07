using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Object/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private int weaponNum;               // ���� ��ȣ
    public int WeaponNum { get { return weaponNum; } }

    [SerializeField]
    private float attackSpeed;         // ���� �ӵ�
    public float AttackSpeed { get { return attackSpeed; } }

    [SerializeField]
    private int level;               // ���� ����
    public int Level { get { return level; } }

    [SerializeField]
    private float range;               // ���� ����
    public float Range { get { return range; } }

    [SerializeField]
    private BulletData bulletData;               // �Ѿ� ������
    public BulletData _BulletData { get { return bulletData; } }
}
