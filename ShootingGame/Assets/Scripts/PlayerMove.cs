using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    // �÷��̾ �̵��� �ӷ�
    public float speed = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);

        // P = P0 + vt �������� ����
        // transform.position = transform.position + dir * speed * Time.deltaTime;
        transform.position += dir * speed * Time.deltaTime;
    }
}
