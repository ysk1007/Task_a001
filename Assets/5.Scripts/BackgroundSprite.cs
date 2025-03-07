using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSprite : MonoBehaviour
{
    public float speed = 5f;                // ��� �̵� �ӵ�
    public float resetPositionX = -37.9f;   // ���ġ�� X ��ġ
    public float startPositionX = 37.9f;    // ���� ��ġ X

    void Update()
    {
        // ����� �������� �̵�
        transform.position += Vector3.left * speed * Time.deltaTime;

        // ����� ���ġ ��ġ�� ������
        if (transform.position.x <= resetPositionX)
        {
            // ����� ���� ��ġ�� ���ġ
            transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
        }
    }
}
