using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �ʿ� �Ӽ� : �̵��ӵ�
    public float speed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���ϱ�
        Vector3 dir = Vector3.up;
        // �̵�, P = P0 + vt �� ����
        transform.position += dir * speed * Time.deltaTime;
    }
}
