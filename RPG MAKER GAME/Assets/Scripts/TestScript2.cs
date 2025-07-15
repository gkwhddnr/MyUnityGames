using System.Collections;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    BGMManager bgmManager;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgmManager = FindFirstObjectByType<BGMManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(abc());
    }

    IEnumerator abc()
    {
        bgmManager.FadeOutMusic();

        yield return new WaitForSeconds(3f);

        bgmManager.FadeInMusic();
    }
}
