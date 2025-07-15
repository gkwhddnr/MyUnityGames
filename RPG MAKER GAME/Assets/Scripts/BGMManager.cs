using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance;

    public AudioClip[] clips;

    private AudioSource source;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);    // �ݺ��� ���� new�� ���� ȣ��ȴٸ� ���� �����ϴ� ���� ����.

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); // ĳ���� ī�޶� ��������
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);   // ī�޶� ���� ���� ����
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(int playMusicTrack)
    {
        source.clip = clips[playMusicTrack];
        source.Play();
    }

    public void Pause()
    {
        source.Pause();
    }

    public void UnPause()
    {
        source.UnPause();
    }

    public void SetVolumn(float volumn)
    {
        source.volume = volumn;
    }

    public void Stop()
    {
        source.Stop();
    }

    public void FadeOutMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }

    IEnumerator FadeOutMusicCoroutine()
    {
        for(float i = 1.0f; i >= 0f; i -= 0.01f)
        {
            source.volume = i;
            yield return waitTime;
            // yield return new WaitForSeconds(0.01f);  �� �ڵ�� �ۼ��ϸ� for�� ���� new�� ��� �����ǹǷ� ��ȿ����
        }
    }

    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }

    IEnumerator FadeInMusicCoroutine()
    {
        for (float i = 0f; i <= 1.0f; i += 0.01f)
        {
            source.volume = i;
            yield return waitTime;
            // yield return new WaitForSeconds(0.01f);  �� �ڵ�� �ۼ��ϸ� for�� ���� new�� ��� �����ǹǷ� ��ȿ����
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
