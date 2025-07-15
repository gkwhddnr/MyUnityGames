using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string trasferMapName;

    public Transform target;
    public BoxCollider2D targetBound;

    private MovingObject thePlayer;     // MoveObject의 CurrentMapName 변수 참조하기 위해서 생성
    private CameraManager theCamera;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thePlayer = FindFirstObjectByType<MovingObject>();
        theCamera = FindFirstObjectByType<CameraManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")   // Collider 영역에 Player가 들어서면
        {
            thePlayer.currentMapName = trasferMapName;
            // SceneManager.LoadScene(trasferMapName); // 다음 씬으로 전환    
            // 빌드하고 시작하면 플레이어가 Collider영역에 들어가면 씬 전환이 안된다.
            // 이를 해결하려면 [File] - [Build Profiles]로 들어가서 "school front"(맵이름)을 Scene List에 추가해야 한다.
            // 그리고 나서 새로 만든 씬의 MainCamera Object를 지워야 한다.

            theCamera.SetBound(targetBound);
            theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = target.transform.position;
        }
    }
}
