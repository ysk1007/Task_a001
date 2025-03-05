using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    [Header("ü��")]
    public float maxHp;
    public float currentHp;

    public GameObject sliderObject;
    public Slider hpSlider;

    [Header("���ݷ�")]
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
    /// ����� �޴� ���
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        // ����� ���
        float hp = currentHp - damage;
        currentHp = hp < 0 ? 0 : hp;

        // �����̴� ������Ʈ
        if (sliderObject != null)
        {
            sliderObject?.SetActive(true);
            hpSlider.value = currentHp;
        }
    }

    
}
