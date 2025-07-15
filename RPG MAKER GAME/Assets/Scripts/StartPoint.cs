using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;   // 맵 이동되면 플레이어가 시작될 위치.
    private MovingObject thePlayer;
    private CameraManager theCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thePlayer = FindFirstObjectByType<MovingObject>();
        theCamera = FindFirstObjectByType<CameraManager>();

        if (startPoint == thePlayer.currentMapName)
        {
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
            // 캐릭터와 함께 카메라가 이동될 때 위에서 아래로 카메라가 이동하므로 부자연스러운 움직임을 제거하기 위해 카메라도 함께 이동함.
            thePlayer.transform.position = this.transform.position;

            // 빌드하고 나서 캐릭터를 각 씬으로 왔다갔다하면 Player Object가 복사되어 캐릭터가 증가되는 문제가 발생한다.
            // 이를 해결하기 위해서 캐릭터가 씬으로 왔다갔다할때 캐릭터를 파괴하고 생성하는 작업이 필요
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
