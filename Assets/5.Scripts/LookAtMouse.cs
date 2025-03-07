using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public Camera mainCamera; // ī�޶� ����

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main; // ���� ī�޶� �ڵ� �Ҵ�
    }

    void Update()
    {
        // ������Ʈ�� ī�޶� ������ �Ÿ� ���
        float distanceToCamera = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ (z ���� �Ÿ��� ����)
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToCamera));

        // ���� ���� ���
        Vector3 direction = mousePos - transform.position;

        // ȸ�� ���� ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ������Ʈ ȸ�� ���� (Z�� ȸ��)
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
