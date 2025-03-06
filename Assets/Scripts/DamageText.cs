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
        animator.SetTrigger("Play");
    }

    public void SetUp(float damage)
    {
        damageText.text = damage.ToString("F0");
    }

    public void ReturnToPool()
    {
        ObjectPool.Instance.ReturnToPool("DamageText",gameObject);
    }

}
