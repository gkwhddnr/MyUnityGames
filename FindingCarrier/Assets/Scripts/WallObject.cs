using UnityEngine;

public class WallObject : MonoBehaviour
{
    void Start()
    {
        if (WallManager.Instance != null)
            WallManager.Instance.Register(gameObject);
    }

    void OnDestroy()
    {
        if(WallManager.Instance != null)
            WallManager.Instance.UnRegister(gameObject);
    }
}
