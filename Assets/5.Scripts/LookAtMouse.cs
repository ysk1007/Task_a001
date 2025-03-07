using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public Camera mainCamera; // 카메라 참조

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main; // 메인 카메라 자동 할당
    }

    void Update()
    {
        // 오브젝트와 카메라 사이의 거리 계산
        float distanceToCamera = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        // 마우스 위치를 월드 좌표로 변환 (z 값을 거리로 설정)
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToCamera));

        // 방향 벡터 계산
        Vector3 direction = mousePos - transform.position;

        // 회전 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 오브젝트 회전 적용 (Z축 회전)
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
