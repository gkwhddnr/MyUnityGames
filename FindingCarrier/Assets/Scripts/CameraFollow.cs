using Unity.Netcode;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    // ������ �Ʒ��� 3��Ī ī�޶�� �����ִ� ��ũ��Ʈ
    private Transform cameraTransform;
    public Vector3 cameraOffset = new Vector3(0, 10, 0);

    // 3��Ī ī�޶�� �� ������ ������������ �� ���� ���ϱ�
    private bool isFollowing = true;

    public float cameraMoveSpeed = 10f;
    public float edgeSize = 20f;

    // ���� ���� -> 3��Ī �þ߷� �̵��ϴ� ������ �������ϰ� �����ֱ�
    public float smoothLerpSpeed = 5f;
    private bool isReturningToPlayer = false;


    void Start()
    {
        // ���� �÷��̾��� ������Ʈ�� ���� ī�޶� ���� ����
        if (IsOwner)
        {
            cameraTransform = Camera.main.transform;
            cameraTransform.position = transform.position + cameraOffset;
            SetCameraPosition(cameraTransform.position);
        }
        else
        {
            // �ڱ��ڽ� �÷��̾ �ƴϸ� ��ũ��Ʈ ��Ȱ��ȭ
            enabled = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Update()
    {
        // Start �Լ����� enabled = false�� ��������� ������ ���� �߰�
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
        // cameraTransform�� null�� �ƴ��� Ȯ��
        if (cameraTransform != null)
        {
            cameraTransform.position = positionToSet;
            cameraTransform.LookAt(transform.position);
        }
    }

    void FreeLookMove()
    {
        if(cameraTransform == null) return;

        // ī�޶� �ٶ󺸴� ����� ������ ������ ���
        Vector3 move = Vector3.zero;
        Vector3 camForward = Vector3.forward;
        Vector3 camRight = Vector3.right;

        Vector3 pos = Input.mousePosition;

        // ��ǻ��ȭ���� �� �����¿�� Ŀ���� �̵��ϸ� ȭ���� ������ (��Ÿũ����Ʈ�� ȭ�� �б� ���)
        if (pos.x >= Screen.width - edgeSize) move += camRight;
        if (pos.x <= edgeSize) move -= camRight;
        if (pos.y >= Screen.height - edgeSize) move += camForward;
        if (pos.y <= edgeSize) move -= camForward;

        cameraTransform.position += move.normalized * cameraMoveSpeed * Time.deltaTime;
    }
}
