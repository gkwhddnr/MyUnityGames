using UnityEngine;

// 오브젝트에 할당된 스크립트 안의 sounds 배열이 안 보이기 때문에 적어야함.
[System.Serializable]   
public class Sound
{
    public string name;     // 사운드 이름

    public AudioClip clip;  // 사운드 파일
    private AudioSource source; // 사운드 플레이어

    public float Volumn;
    public bool loop;

    public void SetSource(AudioSource source)
    {
        this.source = source;
        this.source.clip = clip;
        this.source.loop = loop;
    }

    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetLoop()
    {
        source.loop = true;
    }

    public void SetLoopCancel()
    {
        source.loop = false;
    }

    public void SetVolumn()
    {
        source.volume = Volumn;
    }
}




public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    // 배열을 Unity안에 보이도록 만들어야 함.
    [SerializeField]
    public Sound[] sounds;

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
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + " = " + sounds[i].name);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }

    }

    public void Play(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void Stop(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }
    public void SetLoop(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].SetLoop();
                return;
            }
        }
    }

    public void SetLoopCancel(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].SetLoopCancel();
                return;
            }
        }
    }

    public void SetVolumn(string name, float Volumn)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].Volumn = Volumn;
                sounds[i].SetVolumn();
                return;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
