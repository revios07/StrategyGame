using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    // Reference to prefab of whatever type.
    [SerializeField]
    private T prefab;

    public T GetNewInstance()
    {
        return Instantiate(prefab);
    }
}
