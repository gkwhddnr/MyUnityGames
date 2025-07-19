using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class DoorInteract : NetworkBehaviour
{
    private BoxCollider doorCollider;
    public float interactionRange = 1.75f;
    public KeyCode interactKey = KeyCode.E;

    // 문 사이에 Player가 있는지 확인
    public Transform detectionPoint;
    public Vector3 detectionSize = new Vector3(1f, 2f, 1f);


    // 멀티플레이로 모든 플레이어가 볼 수 있어야 함.
    // 문이 열려 있는지 확인하는 변수
    private NetworkVariable<bool> isOpen = new NetworkVariable<bool>(false);
    // 문이 열리고 있는지 (또는 닫히는 중인지) 확인하는 변수
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

        // 클라이언트에서도 Collider 상태 동기화
        doorCollider.enabled = !isOpen.Value;
    }

    [ServerRpc]
    void RequestToggleDoorServerRpc(ServerRpcParams rpcParans = default)
    {
        if (isBusy.Value) return;
        isBusy.Value = true;    // 잠금

        if (isOpen.Value)
        {
            ulong blockingPlayerId = GetBlockingPlayerId();
            if(blockingPlayerId != ulong.MaxValue)
            {
                // 특정 플레이어에게 메시지 알림
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
        Debug.Log($"[서버] 문 상태 변경: {(isOpen.Value ? "열림" : "닫힘")}");
        // 대기 후 상태 풀기
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
        Debug.Log("<color=yellow> <System> 플레이어 근처에 있어서 문이 닫히지 않았습니다.</color>");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectionPoint != null ? detectionPoint.position : transform.position, detectionSize);
    }

}
