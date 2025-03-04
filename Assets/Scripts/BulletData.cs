using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Data", menuName = "Scriptable Object/Bullet Data")]
public class BulletData : MonoBehaviour
{
    [SerializeField]
    private int damage;         // 총알 대미지
    public int Damage { get { return damage; } }

    [SerializeField]
    private float moveSpeed;    // 총알 속도
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    private float lifeTime;    // 총알 유지 시간
    public float LifeTime { get { return lifeTime; } }

    [SerializeField]
    [Header("관통력")]
    private int penetration;    // 관통력
    public int Penetration { get { return penetration; } }
}
