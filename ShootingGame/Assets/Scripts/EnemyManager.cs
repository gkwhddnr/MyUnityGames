using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 현재 시간
    float currentTime;
    public float createTime = 1; // 일정 시간
    public GameObject enemyFactory; // 적 공장
    float minTime = 1;
    float maxTime = 5;


    void Start()
    {
        // 태어날 적의 생성 시간 설정
        currentTime = UnityEngine.Random.Range(minTime, maxTime);


    }

    // Update is called once per frame
    void Update()
    {
        // 1. 시간이 흐르다가  
        currentTime += Time.deltaTime;
        
        // 2. 현재시간 = 일정 시간
        if(currentTime > createTime)
        {
            // 3. 적 공장에서 적을 생성해
            GameObject enemy = Instantiate(enemyFactory);
            // 내위치에 갖다 놓는다.
            enemy.transform.position = transform.position;

            // 현재 시간 초기화    
            currentTime = 0;

            // 적 생성 후 적 생성 시간 다시 설정
            createTime = UnityEngine.Random.Range(minTime, maxTime);
        }
    }
}
