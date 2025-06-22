using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // ���� �ð�
    float currentTime;
    public float createTime = 1; // ���� �ð�
    public GameObject enemyFactory; // �� ����
    float minTime = 1;
    float maxTime = 5;


    void Start()
    {
        // �¾ ���� ���� �ð� ����
        currentTime = UnityEngine.Random.Range(minTime, maxTime);


    }

    // Update is called once per frame
    void Update()
    {
        // 1. �ð��� �帣�ٰ�  
        currentTime += Time.deltaTime;
        
        // 2. ����ð� = ���� �ð�
        if(currentTime > createTime)
        {
            // 3. �� ���忡�� ���� ������
            GameObject enemy = Instantiate(enemyFactory);
            // ����ġ�� ���� ���´�.
            enemy.transform.position = transform.position;

            // ���� �ð� �ʱ�ȭ    
            currentTime = 0;

            // �� ���� �� �� ���� �ð� �ٽ� ����
            createTime = UnityEngine.Random.Range(minTime, maxTime);
        }
    }
}
