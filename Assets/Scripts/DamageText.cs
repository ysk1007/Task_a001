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
        // �ִϸ����� ����
        animator.SetTrigger("Play");
    }

    /// <summary>
    /// ���ط� �ؽ�Ʈ ����
    /// </summary>
    /// <param name="damage"></param>
    public void SetUp(float damage)
    {
        damageText.text = damage.ToString("F0");
    }

    /// <summary>
    /// ������Ʈ Ǯ�� �����ֱ�
    /// </summary>
    public void ReturnToPool()
    {
        ObjectPool.Instance.ReturnToPool("DamageText",gameObject);
    }

}
