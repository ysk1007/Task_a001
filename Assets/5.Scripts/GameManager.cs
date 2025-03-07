using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("유저 토큰")]
    public int token;                                   // 실제 토큰 갯수

    [Header("토큰 타이머")]
    [HideInInspector] public float tokenTimer;          // 토큰 타이머
    public float tokenDelay;                            // 토큰 충전되는 간격
    public Slider tokenSlider;                          // 토큰 슬라이더 ui
    public TextMeshProUGUI tokenValue;                  // 토큰 텍스트 ui

    [Header("스킬")]
    public List<int> skillPrice;                        // 스킬 가격들을 담고있는 리스트
    public List<TextMeshProUGUI> priceTexts;            // 표시할 텍스트들

    [Header("무기")]
    public List<BoxWeapon> weapons;                     // 등록된 무기

    private void Awake()
    {
        instance = this;
        tokenValue.text = token.ToString();
        tokenSlider.value = Mathf.Clamp(tokenTimer / tokenDelay, 0, 1);

        // 스킬 가격 텍스트에 세팅
        for (int i = 0; i < priceTexts.Count; i++)
        {
            priceTexts[i].text = skillPrice[i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        tokenTimer += Time.deltaTime;

        // tokenSlider의 값을 현재 타이머를 충전 시간으로 나누어 비율로 설정
        tokenSlider.value = Mathf.Clamp(tokenTimer / tokenDelay, 0, 1);

        // 토큰 하나 추가
        if (tokenTimer >= tokenDelay)
        {
            token++;
            tokenValue.text = token.ToString();
            tokenTimer = 0;
        }
    }

    /// <summary>
    /// ui 클릭 스킬
    /// </summary>
    /// <param name="index"></param>
    public void TokenSkill(int index)
    {
        // 토큰 부족해서 리턴
        if (token < skillPrice[index]) return;

        // 필요 비용만큼 감소
        token -= skillPrice[index];
        tokenValue.text = token.ToString();

        // 해당 버튼에 맞는 무기 찾아서 스킬 부르기
        foreach (var weapon in weapons)
        {
            if (weapon == null) return;
            if(weapon.weaponData.WeaponNum == index) weapon.Skill();
        }
    }

    /// <summary>
    /// 씬 초기화
    /// </summary>
    public void SceneReset()
    {
        SceneManager.LoadScene(0);
    }
}
