using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 현재 시간
    float currentTime;
    public float createTime = 1; // 일정 시간
    public GameObject enemyFactory; // 적 공장
    public float minTime = 0.5f;
    public float maxTime = 1.5f;

    // 오브젝트 풀 크기
    public int poolSize = 10;
    // 오브젝트 풀 배열
    // GameObject[] enemyObjectPool;
    public List<GameObject> enemyObjectPool;

    // SpawnPoint들
    public Transform[] spawnPoints;


    void Start()
    {
        createTime = UnityEngine.Random.Range(minTime, maxTime);
        // 태어날 적의 생성 시간 설정
        currentTime = UnityEngine.Random.Range(minTime, maxTime);

        enemyObjectPool = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyFactory);   // 에너미 공장에 에너미 생성
            enemyObjectPool.Add(enemy); // 에너미를 오브젝트 풀에 넣음
            enemy.SetActive(false); // 비활성화
        }

    }

    // Update is called once per frame
    void Update()
    {
        // 1. 시간이 흐르다가  
        currentTime += Time.deltaTime;
        
        // 2. 현재시간 = 일정 시간
        if(currentTime > createTime)
        {
            GameObject enemy = enemyObjectPool[0];  // 오브젝트풀에서 enemy를 가져다 사용
            // 오브젝트 풀에 에너미 존재 시
            if (enemyObjectPool.Count > 0)
            {
                enemyObjectPool.Remove(enemy);  // 오브젝트풀에서 에너미 제거
                int index = UnityEngine.Random.Range(0, spawnPoints.Length);    // 랜덤 인덱스 선택
                enemy.transform.position = spawnPoints[index].position; // 에너미 위치
                enemy.SetActive(true);  // 활성화
            }

            /*
            // 에너미풀안의 에너미들 중에
            for (int i = 0; i < poolSize; i++)
            {
                // 비활성화된 에너미를 찾으면
                GameObject enemy = enemyObjectPool[i];

                if(enemy.activeSelf == false)
                {
                    enemy.transform.position = transform.position;
                    enemy.SetActive(true);  // 활성화 시키고

                    // 랜덤 인덱스 선택
                    int index = Random.Range(0, spawnPoints.Length);

                    // 에너미 위치
                    enemy.transform.position = spawnPoints[index].position;

                    break;  // 활성화했기 때문에 검색 중단
                }
            }
            */
            /*
            // 3. 적 공장에서 적을 생성해
            GameObject enemy = Instantiate(enemyFactory);
            // 내위치에 갖다 놓는다.
            enemy.transform.position = transform.position;
            */

            // 적 생성 후 적 생성 시간 다시 설정
            createTime = UnityEngine.Random.Range(minTime, maxTime);

            // 현재 시간 초기화    
            currentTime = 0;
        }
    }
}
