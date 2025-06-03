using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 필요 속성 : 이동속도
    public float speed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 방향 구하기
        Vector3 dir = Vector3.up;
        // 이동, P = P0 + vt 로 구현
        transform.position += dir * speed * Time.deltaTime;
    }
}
