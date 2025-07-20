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

        // Key G�� ������ �� GlobalNotificationManager.Instance�� null���� Ȯ��
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (IsServer)
            {
                if (GlobalNotificationManager.Instance != null)
                {
                    GlobalNotificationManager.Instance.ShowGlobalMessageClientRpc(" ���� �׽�Ʈ �˸��Դϴ�!");
                }
            }
        }

        // Key P�� ������ �� PersonalNotificationManager.Instance�� null���� Ȯ��
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsServer)
            {
                var targetClient = new ClientRpcParams
                {
                    Send = new ClientRpcSendParams { TargetClientIds = new ulong[] { OwnerClientId } }
                };
                PersonalNotificationManager.Instance.ShowPersonalMessageClientRpc(" ���� �׽�Ʈ �˸��Դϴ�!", targetClient);
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
