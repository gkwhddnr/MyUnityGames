using Unity.Netcode;
using UnityEngine;

public interface IInteractable
{
    void InteractServerRpc(ulong clientId, ServerRpcParams rpcParams = default);
}
