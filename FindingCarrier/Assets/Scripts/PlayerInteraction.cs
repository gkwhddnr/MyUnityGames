using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerInteraction : NetworkBehaviour
{
    public float interactRange = 1f;
    public KeyCode interactKey = KeyCode.E;

    private bool canInteract = true;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (!IsOwner || !canInteract) return;

        if (Input.GetKeyDown(interactKey))
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, interactRange);
            foreach (var hit in hits)
            {
                var interactable = hit.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.InteractServerRpc(NetworkManager.Singleton.LocalClientId);
                    StartCoroutine(InteractionCooldown());
                    break; // 하나만 상호작용
                }
            }

        }
    }

    private IEnumerator InteractionCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(1f);
        canInteract = true;
    }
}