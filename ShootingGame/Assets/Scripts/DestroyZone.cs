using Unity.VisualScripting;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // 영역 안에 다른 물체 감지
    private void OnTriggerEnter(Collider other)
    {
        // 부딪힌 물체가 bullet이거나 enemy라면
        if (other.gameObject.name.Contains("Bullet") || other.gameObject.name.Contains("Enemy"))
        {
            // 부딪힌 물체 비활성화
            other.gameObject.SetActive(false);

            // 부딪힌 물체가 총알일 경우 리스트에 삽입
            if (other.gameObject.name.Contains("Bullet"))
            {
                PlayerFire player = GameObject.Find("Player").GetComponent<PlayerFire>();

                // 리스트에 총알 삽입
                player.bulletObjectPool.Add(other.gameObject);
            }

            else if (other.gameObject.name.Contains("Enemy"))
            {
                // EnemyManager 클래스 얻어오기
                GameObject emObject = GameObject.Find("EnemyManager");
                EnemyManager manager = emObject.GetComponent<EnemyManager>();

                // 리스트에 총알 삽입
                manager.enemyObjectPool.Add(other.gameObject);
            }
        }

        // 물체 없애기
        // Destroy(other.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
