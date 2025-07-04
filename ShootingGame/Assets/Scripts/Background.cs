using UnityEngine;

public class Background : MonoBehaviour
{
    
    public Material bgMaterial; // ��� ��Ƽ����
    public float scrollSpeed = 0.2f;    // ��ũ�� �ӵ�



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Player�� ����ִ� ���� ��� ��ũ���� ��� �̵���.
    void Update()
    {
        Vector2 direction = Vector2.up; // offset�� x,y���� �����ϹǷ� Vector2 ��ü�� ��� ��.

        bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
    }
}
