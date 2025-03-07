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

    /// <summary>
    /// 충돌 감지하는 레이캐스트
    /// </summary>
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

            // 만약 내 앞에 있는 오브젝트 레이어가 타워라면
            if (layer == 9)
            {
                // 공격
                zombie.Attack(true,hit.collider.GetComponent<Status>());
            }
            else
            {
                zombie.Attack(false);
            }

            // 만약 내 앞에 같은 레이어를 가진 좀비가 내 앞을 막고 있다면
            if (layer == transform.parent.gameObject.layer)
            {
                // block 변수 true
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
