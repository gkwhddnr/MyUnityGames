using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // 1. �� �̵�  A (�̺�Ʈ true false) <-> B    ----> database, � ������ ���� true. (���� ����)
    // 2. ���̺�� �ε�
    // 3. �̸� �����θ� ����   ex) ������ �ֱ�

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
