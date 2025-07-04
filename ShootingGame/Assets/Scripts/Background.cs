using UnityEngine;

public class Background : MonoBehaviour
{
    
    public Material bgMaterial; // 배경 머티리얼
    public float scrollSpeed = 0.2f;    // 스크롤 속도



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Player가 살아있는 동안 배경 스크롤이 계속 이동함.
    void Update()
    {
        Vector2 direction = Vector2.up; // offset은 x,y값만 존재하므로 Vector2 객체를 써야 함.

        bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
    }
}
