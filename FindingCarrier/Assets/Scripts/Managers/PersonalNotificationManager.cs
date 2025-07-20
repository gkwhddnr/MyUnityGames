using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PersonalNotificationManager : NetworkBehaviour
{
    public static PersonalNotificationManager Instance { get; private set; }

    [Header("UI Components")]
    public CanvasGroup canvasGroup;
    public Text personalNotificationText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [ClientRpc]
    public void ShowPersonalMessageClientRpc(string message, ClientRpcParams clientRpcParams = default)
    {
        if (!IsOwner) return;  // 각 플레이어 자신에게만 동작

        StopAllCoroutines();
        personalNotificationText.text = message;
        canvasGroup.alpha = 1f;
        canvasGroup.gameObject.SetActive(true);
        StartCoroutine(HideAfterTime());
    }

    private IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(10f);  // 10초 유지

        float fadeDuration = 2f;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }
}
