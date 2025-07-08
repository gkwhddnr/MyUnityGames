using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // ���� �ð�
    float currentTime;
    public float createTime = 1; // ���� �ð�
    public GameObject enemyFactory; // �� ����
    public float minTime = 0.5f;
    public float maxTime = 1.5f;

    // ������Ʈ Ǯ ũ��
    public int poolSize = 10;
    // ������Ʈ Ǯ �迭
    // GameObject[] enemyObjectPool;
    public List<GameObject> enemyObjectPool;

    // SpawnPoint��
    public Transform[] spawnPoints;


    void Start()
    {
        createTime = UnityEngine.Random.Range(minTime, maxTime);
        // �¾ ���� ���� �ð� ����
        currentTime = UnityEngine.Random.Range(minTime, maxTime);

        enemyObjectPool = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyFactory);   // ���ʹ� ���忡 ���ʹ� ����
            enemyObjectPool.Add(enemy); // ���ʹ̸� ������Ʈ Ǯ�� ����
            enemy.SetActive(false); // ��Ȱ��ȭ
        }

    }

    // Update is called once per frame
    void Update()
    {
        // 1. �ð��� �帣�ٰ�  
        currentTime += Time.deltaTime;
        
        // 2. ����ð� = ���� �ð�
        if(currentTime > createTime)
        {
            GameObject enemy = enemyObjectPool[0];  // ������ƮǮ���� enemy�� ������ ���
            // ������Ʈ Ǯ�� ���ʹ� ���� ��
            if (enemyObjectPool.Count > 0)
            {
                enemyObjectPool.Remove(enemy);  // ������ƮǮ���� ���ʹ� ����
                int index = UnityEngine.Random.Range(0, spawnPoints.Length);    // ���� �ε��� ����
                enemy.transform.position = spawnPoints[index].position; // ���ʹ� ��ġ
                enemy.SetActive(true);  // Ȱ��ȭ
            }

            /*
            // ���ʹ�Ǯ���� ���ʹ̵� �߿�
            for (int i = 0; i < poolSize; i++)
            {
                // ��Ȱ��ȭ�� ���ʹ̸� ã����
                GameObject enemy = enemyObjectPool[i];

                if(enemy.activeSelf == false)
                {
                    enemy.transform.position = transform.position;
                    enemy.SetActive(true);  // Ȱ��ȭ ��Ű��

                    // ���� �ε��� ����
                    int index = Random.Range(0, spawnPoints.Length);

                    // ���ʹ� ��ġ
                    enemy.transform.position = spawnPoints[index].position;

                    break;  // Ȱ��ȭ�߱� ������ �˻� �ߴ�
                }
            }
            */
            /*
            // 3. �� ���忡�� ���� ������
            GameObject enemy = Instantiate(enemyFactory);
            // ����ġ�� ���� ���´�.
            enemy.transform.position = transform.position;
            */

            // �� ���� �� �� ���� �ð� �ٽ� ����
            createTime = UnityEngine.Random.Range(minTime, maxTime);

            // ���� �ð� �ʱ�ȭ    
            currentTime = 0;
        }
    }
}
