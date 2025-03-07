using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Object/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private int weaponNum;               // 무기 번호
    public int WeaponNum { get { return weaponNum; } }

    [SerializeField]
    private float attackSpeed;         // 공격 속도
    public float AttackSpeed { get { return attackSpeed; } }

    [SerializeField]
    private int level;               // 무기 레벨
    public int Level { get { return level; } }

    [SerializeField]
    private float range;               // 공격 범위
    public float Range { get { return range; } }

    [SerializeField]
    private BulletData bulletData;               // 총알 데이터
    public BulletData _BulletData { get { return bulletData; } }
}
