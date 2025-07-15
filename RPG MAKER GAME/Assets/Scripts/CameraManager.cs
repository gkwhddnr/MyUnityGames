using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target;   // ī�޶� ���� ���
    public float moveSpeed;     // ī�޶� �̵��ӵ�
    private Vector3 targetPosition;     // ����� ��ġ��

    public BoxCollider2D bound;
    // �ڽ� �ö��̴� ������ �ּ� �ִ� xyz���� ����.
    private Vector3 minBound;
    private Vector3 maxBound;

    // ī�޶��� �ݳʺ�, �ݳ����� ���� ���� ����
    private float halfWidth;
    private float halfHeight;

    // ī�޶��� �ݳ��̰��� ���� �Ӽ��� �̿��ϱ� ���� ����
    private Camera theCamera;


    // ���� �����ϴ� �Լ��̸� 1���� �����.
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
            // this�� Main Camera�� ����Ŵ.

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // 1�ʿ� moveSpeed��ŭ �̵�.
            // lerp�� A���� B�� ������ ���� �������� �߰� ���� ����

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
            // �����ϰ� �����ϸ� ī�޶� �����ȿ� �ִ� ĳ���Ͱ� ������ �������µ� �̴� bound������ ĳ���Ͱ� �浹�� �Ͼ�� �����̴�.
            // �̸� �ذ��Ϸ��� bound���� �Ǵ� ĳ������ box Collider2D�� Is Trigger�� üũ�ؾ� �Ѵ�.
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}