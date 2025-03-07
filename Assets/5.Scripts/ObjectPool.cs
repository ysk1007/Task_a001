using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;  // �̱��� �ν��Ͻ�

    [System.Serializable]
    public class Pool
    {
        public string tag;            // ������Ʈ �±� (Ű�� ����)
        public GameObject prefab;     // ������
        public int size;              // �ʱ� Ǯ ũ��
    }

    [Header("Ǯ ����Ʈ")]
    public List<Pool> pools;            // ���� ���� Ǯ ����Ʈ

    [Space]
    [Header("������ �θ� ��ġ")]
    public Transform objects;           // ������Ʈ ������ �θ�

    private Dictionary<string, Queue<GameObject>> poolDictionary; // Ǯ ���� ��ųʸ�

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        InitializePools();
    }

    /// <summary>
    /// Ǯ �ʱ�ȭ
    /// </summary>
    private void InitializePools()
    {
        // ��ųʸ� �ʱ�ȭ
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // ������ ������Ʈ�� �ʱ� �����ŭ ����
        foreach (Pool pool in pools)
        {
            // ���ο� Ǯ�� ť ����
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity, objects);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            // ��ųʸ��� ������ ť�� �±׸� �߰�
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    /// <summary>
    /// �±׷� ������Ʈ ��������
    /// </summary>
    public GameObject GetFromPool(string tag, Transform pos = null)
    {
        // ���� ��ųʸ��� �����ϴ� ������Ʈ���� Ȯ��
        if (!poolDictionary.ContainsKey(tag)) return null;

        Queue<GameObject> objectPool = poolDictionary[tag];

        // ���� �ִ� ������Ʈ�� ����
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.transform.position = pos.position;
            obj.transform.rotation = pos.rotation;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // ��� Ǯ���� ������Ʈ�� ��� ���̶��, ���� ����
            foreach (Pool pool in pools)
            {
                if (pool.tag == tag)
                {
                    GameObject obj = Instantiate(pool.prefab, pos.position , pos.rotation, objects);
                    obj.SetActive(true);
                    return obj;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// �±׷� ������Ʈ�� ���� ����
    /// </summary>
    public void ReturnToPool(string tag, GameObject obj)
    {
        // ��ųʸ��� �±װ� �����ϴ��� Ȯ��
        if (!poolDictionary.ContainsKey(tag))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
