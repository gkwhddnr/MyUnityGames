using UnityEngine;

// ������Ʈ�� �Ҵ�� ��ũ��Ʈ ���� sounds �迭�� �� ���̱� ������ �������.
[System.Serializable]   
public class Sound
{
    public string name;     // ���� �̸�

    public AudioClip clip;  // ���� ����
    private AudioSource source; // ���� �÷��̾�

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

    // �迭�� Unity�ȿ� ���̵��� ������ ��.
    [SerializeField]
    public Sound[] sounds;

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
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("���� ���� �̸� : " + i + " = " + sounds[i].name);
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
