using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class DoorVisuals : NetworkBehaviour, IInteractable
{
    private BoxCollider doorCollider;

    [SerializeField] private Transform doorModel;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float closeAngle = 0f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private Transform detectionPoint;
    [SerializeField] private Vector3 detectionSize = new Vector3(1f, 2f, 1f);

    private NetworkVariable<bool> isOpen = new NetworkVariable<bool>(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );
    private NetworkVariable<bool> isBusy = new NetworkVariable<bool>(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    private PersonalNotificationManager notificationManagerInstance;

    private void Awake()
    {
        doorCollider = GetComponent<BoxCollider>();
        if (doorModel == null) doorModel = transform;
        if (detectionPoint == null) detectionPoint = transform;
        doorCollider.isTrigger = isOpen.Value;
    }
    void UpdateCollider(bool open)
    {
        doorCollider.isTrigger = open;
    }

    public override void OnNetworkSpawn()
    {
        isOpen.OnValueChanged += OnDoorStateChanged;

        if (IsClient)
        {
            notificationManagerInstance = PersonalNotificationManager.Instance;
            UpdateDoorClientRpc(isOpen.Value);
        }
        OnDoorStateChanged(isOpen.Value,isOpen.Value);
        if(IsServer) UpdateCollider(isOpen.Value);
    }

    public override void OnNetworkDespawn()
    {
        if (NetworkManager.Singleton != null)
        {
            isOpen.OnValueChanged -= OnDoorStateChanged;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void InteractServerRpc(ulong clientId, ServerRpcParams rpcParams = default)
    {
        if (!IsServer || isBusy.Value) return;

        isBusy.Value = true;
        bool targetState = !isOpen.Value;

        if (isOpen.Value && !targetState)
        {
            if (IsPlayerBlocking(out ulong blockingPlayerId))
            {
                DenyCloseClientRpc(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new ulong[] { clientId } } });
                isBusy.Value = false;
                return;
            }
        }

        isOpen.Value = targetState;
        NotifyDoorStateClientRpc(isOpen.Value, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new ulong[] { clientId } } });
        StartCoroutine(ResetBusyAfterDelay());
    }


    private bool IsPlayerBlocking(out ulong blockingPlayerId)
    {
        Collider[] hits = Physics.OverlapBox(detectionPoint.position, detectionSize / 2f, detectionPoint.rotation, LayerMask.GetMask("Player"));

        foreach (var hit in hits)
        {
            var netObj = hit.GetComponentInParent<NetworkObject>();
            if (netObj != null && netObj.IsPlayerObject)
            {
                blockingPlayerId = netObj.OwnerClientId;
                return true;
            }
        }
        blockingPlayerId = ulong.MaxValue;
        return false;
    }

    private void OnDoorStateChanged(bool oldValue, bool newValue)
    {
        UpdateDoorClientRpc(newValue);
    }



    private IEnumerator ResetBusyAfterDelay()
    {
        yield return new WaitForSeconds(1f); // 문 자체 딜레이
        isBusy.Value = false;
    }

    private IEnumerator RotateDoor(bool open)
    {
        Quaternion target = Quaternion.Euler(0, open ? openAngle : closeAngle, 0);
        Quaternion start = doorModel.localRotation;
        float elapsed = 0;

        while (elapsed < 1f)
        {
            doorModel.localRotation = Quaternion.Slerp(start, target, elapsed);
            elapsed += Time.deltaTime * rotationSpeed;
            yield return null;
        }
        doorModel.localRotation = target;

        if (IsServer)
        {
            isBusy.Value = false;
        }
    }

    [ClientRpc]
    private void UpdateDoorClientRpc(bool open)
    {
        StopAllCoroutines();
        StartCoroutine(RotateDoor(open));
        UpdateCollider(open);
    }


    [ClientRpc]
    private void DenyCloseClientRpc(ClientRpcParams rpcParams = default)
    {
        if (notificationManagerInstance != null && notificationManagerInstance.gameObject.activeInHierarchy)
            notificationManagerInstance.ShowPersonalMessage("<color=yellow>플레이어가 문 사이에 있어서 닫히지 않았습니다.</color>");
    }

    [ClientRpc]
    private void NotifyDoorStateClientRpc(bool isNowOpen, ClientRpcParams rpcParams = default)
    {
        if (notificationManagerInstance != null && notificationManagerInstance.gameObject.activeInHierarchy)
        {
            string msg = isNowOpen ? "<color=green>문이 열렸습니다.</color>" : "<color=red>문이 닫혔습니다.</color>";
            notificationManagerInstance.ShowPersonalMessage(msg);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (detectionPoint == null) detectionPoint = transform;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectionPoint.position, detectionSize);
    }
}