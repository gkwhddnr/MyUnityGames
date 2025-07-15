using UnityEngine;

public class Bound : MonoBehaviour
{
    private BoxCollider2D bound;

    private CameraManager theCamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bound = GetComponent<BoxCollider2D>();
        theCamera = FindFirstObjectByType<CameraManager>();
        theCamera.SetBound(bound);
    }
    
}
