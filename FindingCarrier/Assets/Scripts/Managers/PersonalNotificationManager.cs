
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PersonalNotificationManager : NetworkBehaviour
{
    public static PersonalNotificationManager Instance;
    public CanvasGroup canvasGroup;
    public Text personalNotificationText;

    private Coroutine coroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }


    [ClientRpc]
    public void ShowPersonalMessageClientRpc(string message, ClientRpcParams clientRpcParams = default)
    {
        if (!IsOwner) return;  // 각 플레이어 자신에게만 동작

        if (coroutine != null) StopCoroutine(coroutine);
        personalNotificationText.text = message;
        coroutine = StartCoroutine(DisplayRoutine());
    }

    public void ShowPersonalMessage(string message)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        personalNotificationText.text = message;
        coroutine = StartCoroutine(DisplayRoutine());
    }

    private IEnumerator DisplayRoutine()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }
}
