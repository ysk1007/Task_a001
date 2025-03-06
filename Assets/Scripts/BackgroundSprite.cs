using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSprite : MonoBehaviour
{
    public float speed = 5f;                // 배경 이동 속도
    public float resetPositionX = -37.9f;   // 재배치할 X 위치
    public float startPositionX = 37.9f;    // 시작 위치 X

    void Update()
    {
        // 배경을 왼쪽으로 이동
        transform.position += Vector3.left * speed * Time.deltaTime;

        // 배경이 재배치 위치를 지나면
        if (transform.position.x <= resetPositionX)
        {
            // 배경을 시작 위치로 재배치
            transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
        }
    }
}
