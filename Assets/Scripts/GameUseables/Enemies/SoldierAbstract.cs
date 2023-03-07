using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public abstract class SoldierAbstract : SelectableAbstract, IPoolableObject
{
    public SoldierScriptable[] soldierScriptable;
    protected Structs.SoldierStruct soldierData;
    public int soldierHealth;

    public void CanMoveable(Vector3Int movePosition)
    {

    }

    public bool MoveToDirection()
    {
        //CanMoveable()
        if (true)
        {
            return true;
        }

        return false;
    }

    public override void AddToPool()
    {
        //Reset Soldier Health Here
        //Give Random Health To Soldier
        soldierData = GetRandomSoldier();

        base.AddToPool();
    }

    public override Transform UseFromPool()
    {
        base.UseFromPool();

        return this.transform;
    }

    protected Structs.SoldierStruct GetRandomSoldier()
    {
        return soldierScriptable[Random.Range(0, soldierScriptable.Length)].GetSoldierData();
    }
}
