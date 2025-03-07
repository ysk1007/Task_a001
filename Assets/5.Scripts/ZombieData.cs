using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie Data", menuName = "Scriptable Object/Zombie Data")]
public class ZombieData : ScriptableObject
{
    [SerializeField]
    private string tag;         // 좀비 태그
    public string Tag { get { return tag; } }

    [SerializeField]
    private float hp;           // 좀비 체력
    public float Hp { get { return hp; } }

    [SerializeField]
    private float damage;       // 좀비 대미지
    public float Damage { get { return damage; } }

    [SerializeField]
    private float moveSpeed;    // 좀비 이동속도
    public float MoveSpeed { get { return moveSpeed; } }
}
