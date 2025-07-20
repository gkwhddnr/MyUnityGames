using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        Move();

        // Key G를 눌렀을 때 GlobalNotificationManager.Instance가 null인지 확인
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (IsServer)
            {
                if (GlobalNotificationManager.Instance != null)
                {
                    GlobalNotificationManager.Instance.ShowGlobalMessageClientRpc(" 전역 테스트 알림입니다!");
                }
            }
        }

        // Key P를 눌렀을 때 PersonalNotificationManager.Instance가 null인지 확인
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsServer)
            {
                var targetClient = new ClientRpcParams
                {
                    Send = new ClientRpcSendParams { TargetClientIds = new ulong[] { OwnerClientId } }
                };
                PersonalNotificationManager.Instance.ShowPersonalMessageClientRpc(" 개인 테스트 알림입니다!", targetClient);
            }
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(h, 0, v).normalized;
        rb.MovePosition(transform.position + direction * moveSpeed* Time.deltaTime);
    }
}
