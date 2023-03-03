using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public abstract class SelectableAbstract : MonoBehaviour, IPoolableObject
{
    protected virtual void OnSelectedItemFromGame()
    {

    }

    protected virtual void OnItemSelectedFromMenu()
    {

    }

    public void AddToPool()
    {
        throw new System.NotImplementedException();
    }

    public Transform UseFromPool()
    {
        throw new System.NotImplementedException();
    }
}
