using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("유저 토큰")]
    public int token;

    [Header("토큰 타이머")]
    public float tokenTimer;
    public float tokenDelay;
    public Slider tokenSlider;
    public TextMeshProUGUI tokenValue;

    private void Awake()
    {
        instance = this;
        tokenValue.text = token.ToString();
        tokenSlider.value = Mathf.Clamp(tokenTimer / tokenDelay, 0, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tokenTimer += Time.deltaTime;

        // tokenSlider의 값을 현재 타이머를 충전 시간으로 나누어 비율로 설정
        tokenSlider.value = Mathf.Clamp(tokenTimer / tokenDelay, 0, 1);

        if (tokenTimer >= tokenDelay)
        {
            token++;
            tokenValue.text = token.ToString();
            tokenTimer = 0;
        }
    }
}
