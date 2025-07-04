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
        // 값 중 하나 랜덤 설정
        int randValue = UnityEngine.Random.Range(0, 10);

        // 3보다 작으면 플레이어 방향
        if(randValue < 3)
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
        // 폭발 효과 객체 생성
        GameObject explosion = Instantiate(explosionFactory);

        // 폭발 효과 발생
        explosion.transform.position = transform.position;

        // 너 죽고
        Destroy(collision.gameObject);
        // 나 죽자
        Destroy(gameObject);
    }
}
