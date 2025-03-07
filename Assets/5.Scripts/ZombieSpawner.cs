using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner instance;

    public List<Transform> spawnPoint;  // ���� ��ġ�� ���� ����Ʈ

    [Header("���� ���� ����")]
    public int maxSpawnCount;           // �ִ� ������ �� �ִ� ���� ��
    public int currentZombieCount;      // ���� ���� ��

    [Header("���� ����")]
    [HideInInspector] public float spawnTimer;      // ���� Ÿ�̸�
    public float spawnDelay;                        // ���� ����

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
        // ���ݿ� ���缭 ���� ����
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnDelay)
        {
            spawnTimer = 0;
            RandomZomibeSpawn();
        }
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    void RandomZomibeSpawn()
    {
        // ���� �ִ� ������ �� �ִ� ���� ���� �Ѱ����� return
        if (currentZombieCount >= maxSpawnCount) return;

        // ���� ��ȣ�� ���̾� ��ȣ ���� ����
        int zNum = Random.Range(1, 5);
        int layerNum = Random.Range(6, 9);

        // ���� ��ġ�� ���̾ �°� ����
        Transform spawnPos = spawnPoint[layerNum - 6];

        // Ǯ���� ���� �ҷ���
        GameObject zombie = objectPool.GetFromPool("Zombie" + zNum, spawnPos);

        // ���� ���̾� ����
        zombie.layer = layerNum;

        currentZombieCount++;
    }
}
