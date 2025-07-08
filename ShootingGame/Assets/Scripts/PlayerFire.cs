using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    // �Ѿ� ������ ����
    public GameObject bulletFactory;
    // �ѱ�
    public GameObject firePosition;

    // źâ�� �ִ� �Ѿ� ����
    public int poolSize = 10;

    // ������Ʈ Ǯ �迭
    // GameObject[] bulletObjectPool;
    public List<GameObject> bulletObjectPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �¾ �� �Ѿ��� ��� ũ��� ���� �Ѿ� ������ �ݺ��ϰ� �Ѿ��� ������ ������Ʈ Ǯ�� �ִ´�
        bulletObjectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            // �Ѿ� ����
            GameObject bullet = Instantiate(bulletFactory);

            // ������Ʈ Ǯ�� �ֱ�
            // bulletObjectPool[i] = bullet;
            bulletObjectPool.Add(bullet);
            // ��Ȱ��ȭ
            bullet.SetActive(false);
        }

        // ����Ǵ� �÷����� �ȵ���̵��� ��� ���̽�ƽ Ȱ��ȭ
#if UNITY_ANDROID
    GameObject.Find("Joystick canvas XYBZ").SetActive(true);
#elif UNITY_EDITOR || UNITY_STANDALONE
        GameObject.Find("Joystick canvas XYBZ").SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // ����ڰ� �߻� ��ư�� ����
        if (Input.GetButtonDown("Fire1")){
            Fire();



            /*
            for(int i = 0; i < poolSize; i++)
            {
                // ��Ȱ��ȭ�� �Ѿ� ����
                GameObject bullet = bulletObjectPool[i];

                // �Ѿ��� ��Ȱ��ȭ�ƴٸ�
                if(bullet.activeSelf == false)
                {
                    // �Ѿ� Ȱ��ȭ(�߻�)
                    bullet.SetActive(true);

                    // �Ѿ� ��ġ��Ű��
                    bullet.transform.position = transform.position;

                    // �Ѿ� �߻�� ��Ȱ��ȭ �Ѿ� �˻� �ߴ�
                    break;
                }
            }
            */
            /*
            // �Ѿ� ���忡�� �Ѿ� ����.
            GameObject bullet = Instantiate(bulletFactory);
            // �Ѿ� �߻� (�Ѿ��� �ѱ� ��ġ�� ������ ����)
            bullet.transform.position = firePosition.transform.position;
            */
        }
#endif
    }

    public void Fire()
    {
        if (bulletObjectPool.Count > 0)
        {
            // ��Ȱ��ȭ�� �Ѿ� �ϳ� ��������
            GameObject bullet = bulletObjectPool[0];

            // �Ѿ� �߻�(Ȱ��ȭ)
            bullet.SetActive(true);

            // ������Ʈ Ǯ���� �Ѿ� ����
            bulletObjectPool.Remove(bullet);

            // �Ѿ� ��ġ��Ű��
            bullet.transform.position = transform.position;
        }
    }
}
