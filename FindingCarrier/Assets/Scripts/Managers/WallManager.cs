using UnityEngine;

public class WallManager : BaseManager<WallObject>
{
    public static WallManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
