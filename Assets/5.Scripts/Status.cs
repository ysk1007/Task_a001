using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    [Header("�±�")]
    public string tag;

    [Header("ü��")]
    public float maxHp;                 // �ִ� ü��
    public float currentHp;             // ���� ü��

    [Header("HP �����̴�")]
    public GameObject sliderObject;     // �����̴� ������Ʈ
    public Slider hpSlider;             // hp �����̴� ui

    [Header("���ݷ�")]
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

        // ���� ü���� 0�̶��
        if (currentHp == 0) 
        { 
            // Ǯ���� ��ȯ
            ObjectPool.Instance.ReturnToPool(tag, gameObject);

            // ���� �±׸� ���� ������Ʈ��� ���� ����� -1
            if (gameObject.tag == "Zombie") ZombieSpawner.instance.currentZombieCount--;
        }
    }

    /// <summary>
    /// �����̴� ����
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
