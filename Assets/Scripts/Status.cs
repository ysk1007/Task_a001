using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    [Header("태그")]
    public string tag;

    [Header("체력")]
    public float maxHp;
    public float currentHp;

    public GameObject sliderObject;
    public Slider hpSlider;

    [Header("공격력")]
    public float attackDamage;

    public void SetUp(ZombieData zombieData)
    {
        attackDamage = zombieData.Damage;
        maxHp = zombieData.Hp;
        tag = zombieData.Tag;
        if (sliderObject != null)
        {
            sliderObject.SetActive(false);
            hpSlider.maxValue = maxHp;
        }

        currentHp = maxHp;
    }

    /// <summary>
    /// 대미지 받는 기능
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        // 대미지 계산
        float hp = currentHp - damage;
        currentHp = hp < 0 ? 0 : hp;

        // 슬라이더 업데이트
        if (sliderObject != null)
        {
            sliderObject?.SetActive(true);
            hpSlider.value = currentHp;
        }

        if (currentHp == 0) 
        { 
            ObjectPool.Instance.ReturnToPool(tag, gameObject);
            if (gameObject.tag == "Zombie") ZombieSpawner.instance.currentZombieCount--;
        }
    }

    
}
