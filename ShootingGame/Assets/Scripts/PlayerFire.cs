using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    // 총알 생산할 공장
    public GameObject bulletFactory;
    // 총구
    public GameObject firePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자가 발사 버튼을 누름
        if (Input.GetButtonDown("Fire1")){
            // 총알 공장에서 총알 만듦.
            GameObject bullet = Instantiate(bulletFactory);
            // 총알 발사 (총알을 총구 위치로 가져다 놓기)
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
