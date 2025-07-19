using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class DoorInteract : NetworkBehaviour
{
    private BoxCollider doorCollider;
    public float interactionRange = 1.75f;
    public KeyCode interactKey = KeyCode.E;

    // �� ���̿� Player�� �ִ��� Ȯ��
    public Transform detectionPoint;
    public Vector3 detectionSize = new Vector3(1f, 2f, 1f);


    // ��Ƽ�÷��̷� ��� �÷��̾ �� �� �־�� ��.
    // ���� ���� �ִ��� Ȯ���ϴ� ����
    private NetworkVariable<bool> isOpen = new NetworkVariable<bool>(false);
    // ���� ������ �ִ��� (�Ǵ� ������ ������) Ȯ���ϴ� ����
    private NetworkVariable<bool> isBusy = new NetworkVariable<bool>(false);

    private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorCollider = GetComponent<BoxCollider>();
        if (IsOwner)
            gameObject.layer = LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        if (playerTransform == null)
        {
            var foundPlayer = GameObject.FindWithTag("Player");
            if (foundPlayer != null)
            {
                playerTransform = foundPlayer.transform;
            }
            else return;
        }

        if (Vector3.Distance(transform.position, playerTransform.position) <= interactionRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                RequestToggleDoorServerRpc();
            }
        }

        // Ŭ���̾�Ʈ������ Collider ���� ����ȭ
        doorCollider.enabled = !isOpen.Value;
    }

    [ServerRpc]
    void RequestToggleDoorServerRpc(ServerRpcParams rpcParans = default)
    {
        if (isBusy.Value) return;
        isBusy.Value = true;    // ���

        if (isOpen.Value)
        {
            ulong blockingPlayerId = GetBlockingPlayerId();
            if(blockingPlayerId != ulong.MaxValue)
            {
                // Ư�� �÷��̾�� �޽��� �˸�
                DenyDoorCloseClientRpc(new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new ulong[] { blockingPlayerId }
                    }
                });
                isBusy.Value = false;
                return;
            }
        }
        isOpen.Value = !isOpen.Value;
        doorCollider.enabled = !isOpen.Value;
        Debug.Log($"[����] �� ���� ����: {(isOpen.Value ? "����" : "����")}");
        // ��� �� ���� Ǯ��
        StartCoroutine(DoorCooldown());
    }

    IEnumerator DoorCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        isBusy.Value = false;
    }

    ulong GetBlockingPlayerId()
    {
        Collider[] hits = Physics.OverlapBox(detectionPoint != null ? detectionPoint.position : transform.position, detectionSize / 2f, Quaternion.identity, LayerMask.GetMask("Player"));

        foreach (var hit in hits)
        {
            NetworkObject netObj = hit.GetComponent<NetworkObject>();
            if (netObj != null)
                return netObj.OwnerClientId;
        }
        return ulong.MaxValue;
    }

    [ClientRpc]
    void DenyDoorCloseClientRpc(ClientRpcParams clientRpcParams = default)
    {
        Debug.Log("<color=yellow> <System> �÷��̾� ��ó�� �־ ���� ������ �ʾҽ��ϴ�.</color>");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectionPoint != null ? detectionPoint.position : transform.position, detectionSize);
    }

}
