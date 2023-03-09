using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameObjectFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    // Reference to prefab of whatever type
    [HideInInspector]
    protected T prefab;

    public virtual T GetNewInstance(string gameObjectType)
    {
        return Instantiate(prefab);
    }
}
