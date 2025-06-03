using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    // �Ѿ� ������ ����
    public GameObject bulletFactory;
    // �ѱ�
    public GameObject firePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ����ڰ� �߻� ��ư�� ����
        if (Input.GetButtonDown("Fire1")){
            // �Ѿ� ���忡�� �Ѿ� ����.
            GameObject bullet = Instantiate(bulletFactory);
            // �Ѿ� �߻� (�Ѿ��� �ѱ� ��ġ�� ������ ����)
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
