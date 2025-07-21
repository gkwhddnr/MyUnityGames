
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PersonalNotificationManager : NetworkBehaviour
{
    public static PersonalNotificationManager Instance { get; private set; }
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

        /*
        StopAllCoroutines();
        personalNotificationText.text = message;  
        canvasGroup.alpha = 1f;
        canvasGroup.gameObject.SetActive(true);
        StartCoroutine(HideAfterTime());
        */

        if (coroutine != null) StopCoroutine(coroutine);
        personalNotificationText.text = message;
        coroutine = StartCoroutine(ShowMessage());
    }

    public void ShowPersonalMessage(string message)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        personalNotificationText.text = message;
        coroutine = StartCoroutine(ShowMessage());
    }

    private IEnumerator ShowMessage()
    {
        canvasGroup.alpha = 1;
        yield return new WaitForSeconds(2f);
        canvasGroup.alpha = 0;
    }
}
