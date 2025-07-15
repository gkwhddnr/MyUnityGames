using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;   // �� �̵��Ǹ� �÷��̾ ���۵� ��ġ.
    private MovingObject thePlayer;
    private CameraManager theCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thePlayer = FindFirstObjectByType<MovingObject>();
        theCamera = FindFirstObjectByType<CameraManager>();

        if (startPoint == thePlayer.currentMapName)
        {
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
            // ĳ���Ϳ� �Բ� ī�޶� �̵��� �� ������ �Ʒ��� ī�޶� �̵��ϹǷ� ���ڿ������� �������� �����ϱ� ���� ī�޶� �Բ� �̵���.
            thePlayer.transform.position = this.transform.position;

            // �����ϰ� ���� ĳ���͸� �� ������ �Դٰ����ϸ� Player Object�� ����Ǿ� ĳ���Ͱ� �����Ǵ� ������ �߻��Ѵ�.
            // �̸� �ذ��ϱ� ���ؼ� ĳ���Ͱ� ������ �Դٰ����Ҷ� ĳ���͸� �ı��ϰ� �����ϴ� �۾��� �ʿ�
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
