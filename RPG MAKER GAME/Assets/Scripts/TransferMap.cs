using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string trasferMapName;

    public Transform target;
    public BoxCollider2D targetBound;

    private MovingObject thePlayer;     // MoveObject�� CurrentMapName ���� �����ϱ� ���ؼ� ����
    private CameraManager theCamera;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thePlayer = FindFirstObjectByType<MovingObject>();
        theCamera = FindFirstObjectByType<CameraManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")   // Collider ������ Player�� ����
        {
            thePlayer.currentMapName = trasferMapName;
            // SceneManager.LoadScene(trasferMapName); // ���� ������ ��ȯ    
            // �����ϰ� �����ϸ� �÷��̾ Collider������ ���� �� ��ȯ�� �ȵȴ�.
            // �̸� �ذ��Ϸ��� [File] - [Build Profiles]�� ���� "school front"(���̸�)�� Scene List�� �߰��ؾ� �Ѵ�.
            // �׸��� ���� ���� ���� ���� MainCamera Object�� ������ �Ѵ�.

            theCamera.SetBound(targetBound);
            theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = target.transform.position;
        }
    }
}
