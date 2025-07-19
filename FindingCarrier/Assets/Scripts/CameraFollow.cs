using Unity.Netcode;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    // 위에서 아래로 3인칭 카메라로 보여주는 스크립트
    private Transform cameraTransform;
    public Vector3 cameraOffset = new Vector3(0, 10, 0);

    // 3인칭 카메라로 할 것인지 자유시점으로 할 건지 정하기
    private bool isFollowing = true;

    public float cameraMoveSpeed = 10f;
    public float edgeSize = 20f;

    // 자유 시점 -> 3인칭 시야로 이동하는 과정을 스무스하게 보여주기
    public float smoothLerpSpeed = 5f;
    private bool isReturningToPlayer = false;


    void Start()
    {
        // 로컬 플레이어의 오브젝트일 때만 카메라 관련 설정
        if (IsOwner)
        {
            cameraTransform = Camera.main.transform;
            cameraTransform.position = transform.position + cameraOffset;
            SetCameraPosition(cameraTransform.position);
        }
        else
        {
            // 자기자신 플레이어가 아니면 스크립트 비활성화
            enabled = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Update()
    {
        // Start 함수에서 enabled = false로 제어되지만 안전을 위해 추가
        if (!IsOwner || cameraTransform == null) return;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            isFollowing = !isFollowing;

            if (isFollowing)
                isReturningToPlayer = true;
            else
                isReturningToPlayer |= false;
        }

        if (isFollowing)
        {
            Vector3 targetPosition = transform.position + cameraOffset;

            if (isReturningToPlayer || Vector3.Distance(cameraTransform.position, targetPosition) > 0.1f)
            {
                cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, Time.deltaTime * smoothLerpSpeed);
                if(Vector3.Distance(cameraTransform.position, targetPosition) < 0.1f)
                {
                    cameraTransform.position = targetPosition;
                    isReturningToPlayer = false;
                }
            }
            
        }
        else
        {
            FreeLookMove();
        }
    }

    void SetCameraPosition(Vector3 positionToSet)
    {
        // cameraTransform이 null이 아닌지 확인
        if (cameraTransform != null)
        {
            cameraTransform.position = positionToSet;
            cameraTransform.LookAt(transform.position);
        }
    }

    void FreeLookMove()
    {
        if(cameraTransform == null) return;

        // 카메라가 바라보는 방향과 오른쪽 방향을 사용
        Vector3 move = Vector3.zero;
        Vector3 camForward = Vector3.forward;
        Vector3 camRight = Vector3.right;

        Vector3 pos = Input.mousePosition;

        // 컴퓨터화면의 맨 상하좌우로 커서를 이동하면 화면이 움직임 (스타크래프트의 화면 밀기 방식)
        if (pos.x >= Screen.width - edgeSize) move += camRight;
        if (pos.x <= edgeSize) move -= camRight;
        if (pos.y >= Screen.height - edgeSize) move += camForward;
        if (pos.y <= edgeSize) move -= camForward;

        cameraTransform.position += move.normalized * cameraMoveSpeed * Time.deltaTime;
    }
}
