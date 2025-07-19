using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance;

    void Awake() => Instance = this;


}
