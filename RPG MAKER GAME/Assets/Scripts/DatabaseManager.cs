using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // 1. 씬 이동  A (이벤트 true false) <-> B    ----> database, 어떤 변수의 값이 true. (전역 변수)
    // 2. 세이브와 로드
    // 3. 미리 만들어두면 편함   ex) 아이템 넣기

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    public static DatabaseManager instance;


    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

}
