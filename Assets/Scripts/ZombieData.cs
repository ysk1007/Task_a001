using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie Data", menuName = "Scriptable Object/Zombie Data")]
public class ZombieData : ScriptableObject
{
    [SerializeField]
    private string tag;         // ���� �±�
    public string Tag { get { return tag; } }

    [SerializeField]
    private float hp;           // ���� ü��
    public float Hp { get { return hp; } }

    [SerializeField]
    private float damage;       // ���� �����
    public float Damage { get { return damage; } }

    [SerializeField]
    private float moveSpeed;    // ���� �̵��ӵ�
    public float MoveSpeed { get { return moveSpeed; } }
}
