using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // 영역 안에 다른 물체 감지
    private void OnTriggerEnter(Collider other)
    {
        // 물체 없애기
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
