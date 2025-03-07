using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    [Header("태그")]
    public string tag;

    [Header("체력")]
    public float maxHp;                 // 최대 체력
    public float currentHp;             // 현재 체력

    [Header("HP 슬라이더")]
    public GameObject sliderObject;     // 슬라이더 오브젝트
    public Slider hpSlider;             // hp 슬라이더 ui

    [Header("공격력")]
    public float attackDamage;

    private void Awake()
    {
        SliderSetting();
    }

    public void SetUp(ZombieData zombieData)
    {
        attackDamage = zombieData.Damage;
        maxHp = zombieData.Hp;
        tag = zombieData.Tag;

        SliderSetting();
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

        // 현재 체력이 0이라면
        if (currentHp == 0) 
        { 
            // 풀링에 반환
            ObjectPool.Instance.ReturnToPool(tag, gameObject);

            // 좀비 태그를 가진 오브젝트라면 현재 좀비수 -1
            if (gameObject.tag == "Zombie") ZombieSpawner.instance.currentZombieCount--;
        }
    }

    /// <summary>
    /// 슬라이더 세팅
    /// </summary>
    void SliderSetting()
    {
        if (sliderObject != null)
        {
            sliderObject.SetActive(false);
            hpSlider.maxValue = maxHp;
        }

        currentHp = maxHp;
    }
}
