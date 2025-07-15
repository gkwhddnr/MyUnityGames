using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    static public MovingObject instance;    // ���� ���� (�� ����)

    public string currentMapName;   // trasferMap ��ũ��Ʈ�� �ִ� trasferMapName ���� �� ����.

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;     // ��� �Ұ������� �����ϴ� ����

    public string walk_Sound1;
    public string walk_Sound2;
    public string walk_Sound3;
    public string walk_Sound4;

    private AudioManager theAudio;


    public float speed; // ĳ���� ���ǵ�
    private Vector3 vector;     // x,y,z�� ����

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;  // shift�� ������ 2Ÿ�Ͼ� ȭ���̵��ǹǷ� �̸� �����ϰ��� ��������

    // ��ٸ� ������ ��� 1Ÿ�ϴ� 48�ȼ��� �ѹ��� �̵��ؾߵ�.
    public int walkCount;
    private int currentWalkCount;

    // �ڸ�ƾ ���ѽ��� ������ ���ؼ� �ϳ��� ���� ����
    private bool canMove = true;


    // main Character Animation �ȿ� ���� Animator�� Inspector�� �����Ǿ� �ִµ� �̸� ������ ����
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        ó�� ������ ��쿡�� instance�� ���� null�̴�
        ������ ���Ŀ� this ���� �־��� �빮�� �ش� ��ũ��Ʈ�� ����� ��ü�� �� ������ ��� (= ĳ���� ����)
        static���� �� ������ instance ���� this�̱� �빮�� �� ��ü�� ������. (= ĳ���� ���� ����)
        �� static���� ���� ����Ǹ� �� ���� �Ȱ��� ����Ǹ鼭 ���� ĳ������ ���Ӽ� ����
        */
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);     // ĳ���Ͱ� ���� ������ �Ѿ �� �ı��Ǵ� ���� ����
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            theAudio = FindFirstObjectByType<AudioManager>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);   // ĳ���� ������� ����
        }

        
    }

    IEnumerator MoveCoroutine()     // ����ó��(�Լ��� Coroutine�� ���� ����)�ϴ� ��ó�� �ϴ� ���
    {
        while(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))    // ���� shift�� ������
            {
                applyRunSpeed = runSpeed;       // �̵��ӵ� ����
                applyRunFlag = true;
            }
            else // ����
            {
                applyRunSpeed = 0;      // �⺻�ӵ��� ���ư�.   
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)  // �ִϸ��̼� ȥ�� ����
                vector.y = 0;

            // �� �ҷ�����
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            // �浹 ���� Ȯ��
            RaycastHit2D hit;   // ���������� �������� �ּҸ� ��ȯ

            Vector2 start = transform.position; // ĳ���� ���� ��ġ ��

            // ĳ���Ͱ� �̵��ϰ��� �ϴ� ��ġ ��
            Vector2 end = start + new Vector2(vector.x *speed * walkCount, vector.y * speed * walkCount);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);    // �浹�� �Ͼ���� �����Ѵ�.
            boxCollider.enabled = true;

            if (hit.transform != null)  // (������ ���) ���� ������
                break;  // ������� �����ϰ� �� �ڵ�κ��� �������� �ʴ´�.

            // ���� ����
            animator.SetBool("Walking", true);

            int temp = Random.Range(1, 4);
            switch (temp)
            {
                case 1:
                    theAudio.Play(walk_Sound1);
                    break;
                case 2:
                    theAudio.Play(walk_Sound2);
                    break;
                case 3:
                    theAudio.Play(walk_Sound3);
                    break;
                case 4:
                    theAudio.Play(walk_Sound4);
                    break;
            }



            while (currentWalkCount < walkCount)    // 2.4 * 20�� Ű�� ���������� �����.
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0); // vector.x ���� �� ����Ű ��(-1)�̳� �� ����Ű ��(1)�� ���ϵǹǷ� -1*2.4 �Ǵ� 1*2.4�� �ȴ�

                    // �Ǵ� transform.position = vector; �� ���𰡴�
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                    currentWalkCount++;     // ����Ʈ�� ������ ī��Ʈ�� 2�� ����

                currentWalkCount++; // �⺻������ ī��Ʈ 1�� ����
                yield return new WaitForSeconds(0.01f);    // 0.01�ʵ��� ��� (�� �ڿ������� ������)
            }
            currentWalkCount = 0;

            // �������ϸ� �ִϸ��̼��� ���ڿ������� ����.
            // �̸� �ذ��Ϸ��� Animator�� �� Ʈ���� ����� Transition�� Has Exit Time üũ�� �����ϰ�
            // Trasition Duration ���� 0���� �����ؾ���. �׷��� �ý����� �ﰢ������ �ֽ�ȭ��.
        }

        animator.SetBool("Walking", false);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());    // �� ���·� �����ϰ� �����ϸ� �ڸ�ƾ�� ���ѽ����ϴ� ������ �߻���.
                
            }
        }
    }

    internal void Move(string direction)
    {
        throw new System.NotImplementedException();
    }

    internal bool checkcollision()
    {
        throw new System.NotImplementedException();
    }
}
