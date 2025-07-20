using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalNotificationManager : NetworkBehaviour
{
    public static GlobalNotificationManager Instance { get; private set; }

    [Header("UI Components")]
    public CanvasGroup canvasGroup;
    public Text globalNotificationText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [ClientRpc]
    public void ShowGlobalMessageClientRpc(string message)
    {
        StopAllCoroutines();
        globalNotificationText.text = message;
        canvasGroup.alpha = 1f;
        canvasGroup.gameObject.SetActive(true);
        StartCoroutine(HideAfterTime());
    }

    private IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(10f);

        float fadeDuration = 2f;
        float currentTime = 0f;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
            yield return null;
        }

        canvasGroup.gameObject.SetActive(false);
    }
}
