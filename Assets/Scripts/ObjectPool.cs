using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;  // 싱글톤 인스턴스

    [System.Serializable]
    public class Pool
    {
        public string tag;            // 오브젝트 태그 (키값 역할)
        public GameObject prefab;     // 프리팹
        public int size;              // 초기 풀 크기
    }

    [Header("풀 리스트")]
    public List<Pool> pools;            // 여러 개의 풀 리스트

    [Space]
    [Header("관리할 부모 위치")]
    public Transform objects;           // 오브젝트 관리할 부모

    private Dictionary<string, Queue<GameObject>> poolDictionary; // 풀 관리 딕셔너리

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        InitializePools();
    }

    private void InitializePools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity, objects);
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetFromPool(string tag, Transform pos = null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        Queue<GameObject> objectPool = poolDictionary[tag];

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
            // 모든 풀링된 오브젝트가 사용 중이라면, 새로 생성
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

    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
