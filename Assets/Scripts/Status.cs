using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    [Header("체력")]
    public float maxHp;
    public float currentHp;

    public GameObject sliderObject;
    public Slider hpSlider;

    [Header("공격력")]
    public float attackDamage;

    private void OnEnable()
    {
        if (sliderObject != null)
        {
            sliderObject.SetActive(false);
            hpSlider.maxValue = maxHp;
        }

        currentHp = maxHp;
    }

    public void SetUp()
    {

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
    }

    
}
