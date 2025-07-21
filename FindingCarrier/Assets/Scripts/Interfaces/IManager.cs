using System.Collections.Generic;
using UnityEngine;


public interface IManager
{
    void Register(GameObject obj);
    void UnRegister(GameObject obj);
    List<GameObject> GetAll();
}
