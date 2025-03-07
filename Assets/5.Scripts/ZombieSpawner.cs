using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner instance;

    public List<Transform> spawnPoint;  // 스폰 위치를 담은 리스트

    [Header("스폰 갯수 관련")]
    public int maxSpawnCount;           // 최대 스폰할 수 있는 좀비 수
    public int currentZombieCount;      // 현재 좀비 수

    [Header("스폰 간격")]
    [HideInInspector] public float spawnTimer;      // 스폰 타이머
    public float spawnDelay;                        // 스폰 간격

    ObjectPool objectPool;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        objectPool = ObjectPool.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // 간격에 맞춰서 좀비 생성
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnDelay)
        {
            spawnTimer = 0;
            RandomZomibeSpawn();
        }
    }

    /// <summary>
    /// 랜덤 좀비 생성
    /// </summary>
    void RandomZomibeSpawn()
    {
        // 만약 최대 스폰할 수 있는 좀비 수를 넘겼으면 return
        if (currentZombieCount >= maxSpawnCount) return;

        // 좀비 번호와 레이어 번호 랜덤 설정
        int zNum = Random.Range(1, 5);
        int layerNum = Random.Range(6, 9);

        // 스폰 위치를 레이어에 맞게 설정
        Transform spawnPos = spawnPoint[layerNum - 6];

        // 풀에서 좀비 불러옴
        GameObject zombie = objectPool.GetFromPool("Zombie" + zNum, spawnPos);

        // 좀비 레이어 설정
        zombie.layer = layerNum;

        currentZombieCount++;
    }
}
