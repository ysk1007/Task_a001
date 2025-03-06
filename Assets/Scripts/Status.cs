using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    [Header("�±�")]
    public string tag;

    [Header("ü��")]
    public float maxHp;
    public float currentHp;

    public GameObject sliderObject;
    public Slider hpSlider;

    [Header("���ݷ�")]
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

        if (currentHp == 0) 
        { 
            ObjectPool.Instance.ReturnToPool(tag, gameObject);
            if (gameObject.tag == "Zombie") ZombieSpawner.instance.currentZombieCount--;
        }
    }

    
}
