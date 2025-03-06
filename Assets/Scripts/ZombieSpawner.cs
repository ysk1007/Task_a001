using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner instance;
    public List<Transform> spawnPoint;

    [Header("스폰 갯수 관련")]
    public int maxSpawnCount;
    public int currentZombieCount;

    [Header("스폰 시간")]
    public float spawnTimer;
    public float spawnDelay;

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
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnDelay)
        {
            spawnTimer = 0;
            RandomZomibeSpawn();
        }
    }

    void RandomZomibeSpawn()
    {
        if (currentZombieCount >= maxSpawnCount) return;

        int zNum = Random.Range(1, 5);
        int layerNum = Random.Range(6, 9);
        Transform spawnPos = spawnPoint[layerNum - 6];

        GameObject zombie = objectPool.GetFromPool("Zombie" + zNum, spawnPos);
        zombie.layer = layerNum;
        currentZombieCount++;
    }
}
