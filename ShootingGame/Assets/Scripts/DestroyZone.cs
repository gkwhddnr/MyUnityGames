using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // ���� �ȿ� �ٸ� ��ü ����
    private void OnTriggerEnter(Collider other)
    {
        // ��ü ���ֱ�
        Destroy(other.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
