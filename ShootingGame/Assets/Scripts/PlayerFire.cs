using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    // 총알 생산할 공장
    public GameObject bulletFactory;
    // 총구
    public GameObject firePosition;

    // 탄창에 넣는 총알 개수
    public int poolSize = 10;

    // 오브젝트 풀 배열
    // GameObject[] bulletObjectPool;
    public List<GameObject> bulletObjectPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 태어날 때 총알을 담는 크기와 넣을 총알 개수를 반복하고 총알을 생성해 오브젝트 풀에 넣는다
        bulletObjectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            // 총알 생성
            GameObject bullet = Instantiate(bulletFactory);

            // 오브젝트 풀에 넣기
            // bulletObjectPool[i] = bullet;
            bulletObjectPool.Add(bullet);
            // 비활성화
            bullet.SetActive(false);
        }

        // 실행되는 플랫폼이 안드로이드일 경우 조이스틱 활성화
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
        // 사용자가 발사 버튼을 누름
        if (Input.GetButtonDown("Fire1")){
            Fire();



            /*
            for(int i = 0; i < poolSize; i++)
            {
                // 비활성화된 총알 선택
                GameObject bullet = bulletObjectPool[i];

                // 총알이 비활성화됐다면
                if(bullet.activeSelf == false)
                {
                    // 총알 활성화(발사)
                    bullet.SetActive(true);

                    // 총알 위치시키기
                    bullet.transform.position = transform.position;

                    // 총알 발사로 비활성화 총알 검색 중단
                    break;
                }
            }
            */
            /*
            // 총알 공장에서 총알 만듦.
            GameObject bullet = Instantiate(bulletFactory);
            // 총알 발사 (총알을 총구 위치로 가져다 놓기)
            bullet.transform.position = firePosition.transform.position;
            */
        }
#endif
    }

    public void Fire()
    {
        if (bulletObjectPool.Count > 0)
        {
            // 비활성화된 총알 하나 가져오기
            GameObject bullet = bulletObjectPool[0];

            // 총알 발사(활성화)
            bullet.SetActive(true);

            // 오브젝트 풀에서 총알 제거
            bulletObjectPool.Remove(bullet);

            // 총알 위치시키기
            bullet.transform.position = transform.position;
        }
    }
}
