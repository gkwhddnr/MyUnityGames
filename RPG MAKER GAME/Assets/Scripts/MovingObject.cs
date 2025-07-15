using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    static public MovingObject instance;    // 정적 변수 (값 공유)

    public string currentMapName;   // trasferMap 스크립트에 있는 trasferMapName 변수 값 저장.

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;     // 통과 불가능한지 판정하는 변수

    public string walk_Sound1;
    public string walk_Sound2;
    public string walk_Sound3;
    public string walk_Sound4;

    private AudioManager theAudio;


    public float speed; // 캐릭터 스피드
    private Vector3 vector;     // x,y,z축 설정

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;  // shift를 누르면 2타일씩 화면이동되므로 이를 방지하고자 변수생성

    // 쯔꾸르 게임의 경우 1타일당 48픽셀을 한번에 이동해야됨.
    public int walkCount;
    private int currentWalkCount;

    // 코르틴 무한실행 방지를 위해서 하나의 변수 선언
    private bool canMove = true;


    // main Character Animation 안에 보면 Animator가 Inspector에 장착되어 있는데 이를 제어할 변수
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        처음 생성된 경우에만 instance의 값이 null이다
        생성된 이후에 this 값을 주었기 대문에 해당 스크립트가 적용된 객체가 또 생성될 경우 (= 캐릭터 복제)
        static으로 값 공유한 instance 값이 this이기 대문에 그 객체는 삭제됨. (= 캐릭터 복제 방지)
        ∴ static으로 값이 변경되면 그 값도 똑같이 변경되면서 기존 캐릭터의 종속성 유지
        */
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);     // 캐릭터가 다음 씬으로 넘어갈 때 파괴되는 것을 방지
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            theAudio = FindFirstObjectByType<AudioManager>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);   // 캐릭터 복사생성 방지
        }

        
    }

    IEnumerator MoveCoroutine()     // 다중처리(함수와 Coroutine이 동시 실행)하는 것처럼 하는 기능
    {
        while(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))    // 왼쪽 shift를 누르면
            {
                applyRunSpeed = runSpeed;       // 이동속도 증가
                applyRunFlag = true;
            }
            else // 떼면
            {
                applyRunSpeed = 0;      // 기본속도로 돌아감.   
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)  // 애니메이션 혼선 방지
                vector.y = 0;

            // 값 불러오기
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            // 충돌 판정 확인
            RaycastHit2D hit;   // 시작지점과 끝지점의 주소를 반환

            Vector2 start = transform.position; // 캐릭터 현재 위치 값

            // 캐릭터가 이동하고자 하는 위치 값
            Vector2 end = start + new Vector2(vector.x *speed * walkCount, vector.y * speed * walkCount);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);    // 충돌이 일어나는지 판정한다.
            boxCollider.enabled = true;

            if (hit.transform != null)  // (게임의 경우) 벽을 만나면
                break;  // 여기까지 실행하고 밑 코드부분은 실행하지 않는다.

            // 상태 전이
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



            while (currentWalkCount < walkCount)    // 2.4 * 20이 키를 누를때마다 실행됨.
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0); // vector.x 값은 좌 방향키 값(-1)이나 우 방향키 값(1)이 리턴되므로 -1*2.4 또는 1*2.4가 된다

                    // 또는 transform.position = vector; 로 선언가능
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                    currentWalkCount++;     // 쉬프트를 누르면 카운트가 2씩 증가

                currentWalkCount++; // 기본적으로 카운트 1씩 증가
                yield return new WaitForSeconds(0.01f);    // 0.01초동안 대기 (∵ 자연스러운 움직임)
            }
            currentWalkCount = 0;

            // 컴파일하면 애니메이션이 부자연스럽게 나옴.
            // 이를 해결하려면 Animator의 각 트리에 연결된 Transition에 Has Exit Time 체크를 해제하고
            // Trasition Duration 값을 0으로 설정해야함. 그러면 시스템이 즉각적으로 최신화함.
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
                StartCoroutine(MoveCoroutine());    // 이 상태로 선언하고 실행하면 코르틴이 무한실행하는 문제가 발생함.
                
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
