using UnityEngine;

public class TestScript1 : MonoBehaviour
{
    BGMManager bgmManager;

    public int playMusicTrack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgmManager = FindFirstObjectByType<BGMManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bgmManager.Play(playMusicTrack);
        this.gameObject.SetActive(false);   // BGM 재생성 방지
    }
}
