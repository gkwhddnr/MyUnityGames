using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance;

    public AudioClip[] clips;

    private AudioSource source;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);    // 반복문 내에 new가 자주 호출된다면 따로 선언하는 것이 좋음.

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); // 캐릭터 카메라도 마찬가지
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);   // 카메라 복사 생성 방지
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
            // yield return new WaitForSeconds(0.01f);  이 코드로 작성하면 for문 내에 new가 계속 생성되므로 비효율적
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
            // yield return new WaitForSeconds(0.01f);  이 코드로 작성하면 for문 내에 new가 계속 생성되므로 비효율적
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
