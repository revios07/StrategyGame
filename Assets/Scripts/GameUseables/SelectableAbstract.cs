using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public abstract class SelectableAbstract : MonoBehaviour, ISelectableObject
{
    public Enums.ObjectType objectType;

    public virtual void OnSelectedItemFromGame()
    {

    }

    public virtual void OnItemSelectedFromMenu()
    {

    }

    public virtual void AddToPool()
    {
        
    }

    public virtual Transform UseFromPool()
    {
        return this.transform;
    }
}
