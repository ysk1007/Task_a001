using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("���� ��ū")]
    public int token;                                   // ���� ��ū ����

    [Header("��ū Ÿ�̸�")]
    [HideInInspector] public float tokenTimer;          // ��ū Ÿ�̸�
    public float tokenDelay;                            // ��ū �����Ǵ� ����
    public Slider tokenSlider;                          // ��ū �����̴� ui
    public TextMeshProUGUI tokenValue;                  // ��ū �ؽ�Ʈ ui

    [Header("��ų")]
    public List<int> skillPrice;                        // ��ų ���ݵ��� ����ִ� ����Ʈ
    public List<TextMeshProUGUI> priceTexts;            // ǥ���� �ؽ�Ʈ��

    [Header("����")]
    public List<BoxWeapon> weapons;                     // ��ϵ� ����

    private void Awake()
    {
        instance = this;
        tokenValue.text = token.ToString();
        tokenSlider.value = Mathf.Clamp(tokenTimer / tokenDelay, 0, 1);

        // ��ų ���� �ؽ�Ʈ�� ����
        for (int i = 0; i < priceTexts.Count; i++)
        {
            priceTexts[i].text = skillPrice[i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        tokenTimer += Time.deltaTime;

        // tokenSlider�� ���� ���� Ÿ�̸Ӹ� ���� �ð����� ������ ������ ����
        tokenSlider.value = Mathf.Clamp(tokenTimer / tokenDelay, 0, 1);

        // ��ū �ϳ� �߰�
        if (tokenTimer >= tokenDelay)
        {
            token++;
            tokenValue.text = token.ToString();
            tokenTimer = 0;
        }
    }

    /// <summary>
    /// ui Ŭ�� ��ų
    /// </summary>
    /// <param name="index"></param>
    public void TokenSkill(int index)
    {
        // ��ū �����ؼ� ����
        if (token < skillPrice[index]) return;

        // �ʿ� ��븸ŭ ����
        token -= skillPrice[index];
        tokenValue.text = token.ToString();

        // �ش� ��ư�� �´� ���� ã�Ƽ� ��ų �θ���
        foreach (var weapon in weapons)
        {
            if (weapon == null) return;
            if(weapon.weaponData.WeaponNum == index) weapon.Skill();
        }
    }

    /// <summary>
    /// �� �ʱ�ȭ
    /// </summary>
    public void SceneReset()
    {
        SceneManager.LoadScene(0);
    }
}
