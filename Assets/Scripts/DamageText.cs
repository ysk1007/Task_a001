using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public Animator animator;

    private void OnEnable()
    {
        // 애니메이터 실행
        animator.SetTrigger("Play");
    }

    /// <summary>
    /// 피해량 텍스트 셋팅
    /// </summary>
    /// <param name="damage"></param>
    public void SetUp(float damage)
    {
        damageText.text = damage.ToString("F0");
    }

    /// <summary>
    /// 오브젝트 풀에 돌려주기
    /// </summary>
    public void ReturnToPool()
    {
        ObjectPool.Instance.ReturnToPool("DamageText",gameObject);
    }

}
