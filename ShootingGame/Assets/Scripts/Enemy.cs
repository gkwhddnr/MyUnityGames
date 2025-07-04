using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �ʿ� �Ӽ�: �̵��ӵ�
    public float speed = 5.0f;
    GameObject player;
    Vector3 dir;

    // ���� �ּ�(�ܺΰ� ����)
    public GameObject explosionFactory;

    void Start()
    {
        // �� �� �ϳ� ���� ����
        int randValue = UnityEngine.Random.Range(0, 10);

        // 3���� ������ �÷��̾� ����
        if(randValue < 3)
        {
            // �÷��̾ target���� ����
            GameObject target = GameObject.Find("Player");
            // ���ⱸ�ϱ�
            dir = target.transform.position - transform.position;
            // ���� ũ�� 1�� ����
            dir.Normalize();
        }
        else // �׷��� ������ �Ʒ� ����
        {
            dir = Vector3.down;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �̵�
        transform.position += dir * speed * Time.deltaTime;
    }

    // �浹����
    private void OnCollisionEnter(Collision collision)
    {
        // ���� ȿ�� ��ü ����
        GameObject explosion = Instantiate(explosionFactory);

        // ���� ȿ�� �߻�
        explosion.transform.position = transform.position;

        // �� �װ�
        Destroy(collision.gameObject);
        // �� ����
        Destroy(gameObject);
    }
}
