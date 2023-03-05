using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public abstract class SelectableAbstract : MonoBehaviour, IPoolableObject
{
    public virtual void OnSelectedItemFromGame()
    {

    }

    public virtual void OnItemSelectedFromMenu()
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
