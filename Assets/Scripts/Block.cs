using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float rayLength = 1f; // 레이캐스트 길이
    public LayerMask layerMask; // 충돌 감지할 레이어 설정

    Zombie zombie;

    private void Awake()
    {
        zombie = GetComponentInParent<Zombie>();
    }

    private void Update()
    {
        ShootRaycast();
    }

    void ShootRaycast()
    {
        Vector2 origin = transform.position; // 레이캐스트 시작점
        Vector2 direction = Vector2.left; // 오른쪽 방향


        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength); // 레이캐스트 발사

        // 레이캐스트 경로를 Scene 뷰에 시각적으로 표시
        Debug.DrawLine(origin, origin + direction * rayLength, Color.red, 1f); // 1초 동안 빨간색 선으로 표시

        if (hit.collider != null)
        {
            int layer = hit.collider.gameObject.layer;

            if (layer == 9)
            {
                zombie.Attack(true,hit.collider.GetComponent<Status>());
            }
            else
            {
                zombie.Attack(false);
            }

            if (layer == transform.parent.gameObject.layer)
            {
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
