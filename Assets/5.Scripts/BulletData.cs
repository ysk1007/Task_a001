using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Data", menuName = "Scriptable Object/Bullet Data")]
public class BulletData : ScriptableObject
{
    [SerializeField]
    private string bulletTag;           // �Ѿ� �±�
    public string BulletTag { get { return bulletTag; } }

    [SerializeField]
    private float damage;           // �Ѿ� �����
    public float Damage { get { return damage; } }

    [SerializeField]
    private float moveSpeed;        // �Ѿ� �ӵ�
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    private float lifeTime;         // �Ѿ� ���� �ð�
    public float LifeTime { get { return lifeTime; } }

    [SerializeField]
    [Header("�����")]
    private int penetration;        // �����
    public int Penetration { get { return penetration; } }
}
