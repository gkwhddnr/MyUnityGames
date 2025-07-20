using UnityEngine;

public class DoorManager : BaseManager<DoorInteract>
{
    public static DoorManager Instance { get; private set; }

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
