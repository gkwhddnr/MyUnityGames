using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target;   // 카메라가 따라갈 대상
    public float moveSpeed;     // 카메라 이동속도
    private Vector3 targetPosition;     // 대상의 위치값

    public BoxCollider2D bound;
    // 박스 컬라이더 영역의 최소 최대 xyz값을 지님.
    private Vector3 minBound;
    private Vector3 maxBound;

    // 카메라의 반너비, 반높이의 값을 지닐 변수
    private float halfWidth;
    private float halfHeight;

    // 카메라의 반높이값을 구할 속성을 이용하기 위한 변수
    private Camera theCamera;


    // 최초 실행하는 함수이며 1번만 실행됨.
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
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            // this는 Main Camera를 가리킴.

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // 1초에 moveSpeed만큼 이동.
            // lerp는 A값과 B값 사이의 선형 보간으로 중간 값을 리턴

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
            // 빌드하고 실행하면 카메라 영역안에 있는 캐릭터가 밑으로 내려가는데 이는 bound영역과 캐릭터가 충돌이 일어나는 현상이다.
            // 이를 해결하려면 bound영역 또는 캐릭터의 box Collider2D에 Is Trigger를 체크해야 한다.
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}