using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float rayLength = 1f; // ����ĳ��Ʈ ����
    public LayerMask layerMask; // �浹 ������ ���̾� ����

    Zombie zombie;

    private void Awake()
    {
        zombie = GetComponentInParent<Zombie>();
    }

    private void Update()
    {
        ShootRaycast();
    }

    /// <summary>
    /// �浹 �����ϴ� ����ĳ��Ʈ
    /// </summary>
    void ShootRaycast()
    {
        Vector2 origin = transform.position; // ����ĳ��Ʈ ������
        Vector2 direction = Vector2.left; // ������ ����


        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength); // ����ĳ��Ʈ �߻�

        // ����ĳ��Ʈ ��θ� Scene �信 �ð������� ǥ��
        Debug.DrawLine(origin, origin + direction * rayLength, Color.red, 1f); // 1�� ���� ������ ������ ǥ��

        if (hit.collider != null)
        {
            int layer = hit.collider.gameObject.layer;

            // ���� �� �տ� �ִ� ������Ʈ ���̾ Ÿ�����
            if (layer == 9)
            {
                // ����
                zombie.Attack(true,hit.collider.GetComponent<Status>());
            }
            else
            {
                zombie.Attack(false);
            }

            // ���� �� �տ� ���� ���̾ ���� ���� �� ���� ���� �ִٸ�
            if (layer == transform.parent.gameObject.layer)
            {
                // block ���� true
                zombie.block = true;
            }
            else
            {
                zombie.block = false;
            }
        }
        else
        {
            zombie.Attack(false);
        }
    }
}
