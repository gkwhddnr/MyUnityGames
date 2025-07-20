using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager<T> : MonoBehaviour, IManager where T : Component
{
    protected List<T> managedObjects = new List<T>();

    public virtual void Register(GameObject obj)
    {
        T component = obj.GetComponent<T>();
        if(component != null && !managedObjects.Contains(component))
            managedObjects.Add(component);
    }

    public virtual void UnRegister(GameObject obj)
    {
        T component = obj.GetComponent<T>();
        if (component != null)
            managedObjects.Remove(component);
    }

    public List<GameObject> GetAll()
    {
        List<GameObject> all = new List<GameObject>();
        foreach (var item in managedObjects)
            if (item != null) all.Add(item.gameObject);
        return all;
    }

    public int Count() => managedObjects.Count;
}
