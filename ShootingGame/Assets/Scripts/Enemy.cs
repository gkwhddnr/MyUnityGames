using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 필요 속성: 이동속도
    public float speed = 5.0f;
    GameObject player;
    Vector3 dir;

    // 폭발 주소(외부값 삽입)
    public GameObject explosionFactory;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        // 값 중 하나 랜덤 설정
        int randValue = UnityEngine.Random.Range(0, 10);

        // 3보다 작으면 플레이어 방향
        if (randValue < 3)
        {
            // 플레이어를 target으로 설정
            GameObject target = GameObject.Find("Player");
            // 방향구하기
            dir = target.transform.position - transform.position;
            // 방향 크기 1로 설정
            dir.Normalize();
        }
        else // 그렇지 않으면 아래 방향
        {
            dir = Vector3.down;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 이동
        transform.position += dir * speed * Time.deltaTime;
    }

    // 충돌시작
    private void OnCollisionEnter(Collision collision)
    {
        // 에너미 잡을 때마다 현재 점수 표시
        // ScoreManager.Instance.SetScore(ScoreManager.Instance.GetScore() + 1);
        ScoreManager.Instance.Score++;

        /*
        // 씬에서 ScoreManager 객체 찾기
        GameObject smObject = GameObject.Find("ScoreManager");

        // ScoreManager 게임 오브젝트를 얻어오기
        ScoreManager sm = GetComponent<ScoreManager>();

        // ScoreManager의 Get/Set 함수 설정
        sm.SetScore(sm.GetScore() + 1);
        */

        // 폭발 효과 객체 생성
        GameObject explosion = Instantiate(explosionFactory);

        // 폭발 효과 발생
        explosion.transform.position = transform.position;

        // 부딪힌 물체가 bullet이라면
        if (collision.gameObject.name.Contains("Bullet"))
        {
            // 부딪힌 물체 비활성화
            collision.gameObject.SetActive(false);

            // PlayerFire 클래스 얻어오기
            PlayerFire player = GameObject.Find("Player").GetComponent<PlayerFire>();

            // 리스트에 총알 삽입
            player.bulletObjectPool.Add(collision.gameObject);
        }
        // 그렇지 않으면 제거
        else
        {
            // 너 죽고
            Destroy(collision.gameObject);
        }

        // 나 죽자
        // Destroy(gameObject);
        // 없애는 대신 비활성화해 풀에 자원 반납한다.
        gameObject.SetActive(false);

        // EnemyManager 클래스 얻어오기
        GameObject emObject = GameObject.Find("EnemyManager");
        EnemyManager manager = emObject.GetComponent<EnemyManager>();

        // 리스트에 총알 삽입
        manager.enemyObjectPool.Add(collision.gameObject);
    }
}
