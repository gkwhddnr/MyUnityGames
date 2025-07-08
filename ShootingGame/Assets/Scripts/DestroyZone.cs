using Unity.VisualScripting;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // ���� �ȿ� �ٸ� ��ü ����
    private void OnTriggerEnter(Collider other)
    {
        // �ε��� ��ü�� bullet�̰ų� enemy���
        if (other.gameObject.name.Contains("Bullet") || other.gameObject.name.Contains("Enemy"))
        {
            // �ε��� ��ü ��Ȱ��ȭ
            other.gameObject.SetActive(false);

            // �ε��� ��ü�� �Ѿ��� ��� ����Ʈ�� ����
            if (other.gameObject.name.Contains("Bullet"))
            {
                PlayerFire player = GameObject.Find("Player").GetComponent<PlayerFire>();

                // ����Ʈ�� �Ѿ� ����
                player.bulletObjectPool.Add(other.gameObject);
            }

            else if (other.gameObject.name.Contains("Enemy"))
            {
                // EnemyManager Ŭ���� ������
                GameObject emObject = GameObject.Find("EnemyManager");
                EnemyManager manager = emObject.GetComponent<EnemyManager>();

                // ����Ʈ�� �Ѿ� ����
                manager.enemyObjectPool.Add(other.gameObject);
            }
        }

        // ��ü ���ֱ�
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
