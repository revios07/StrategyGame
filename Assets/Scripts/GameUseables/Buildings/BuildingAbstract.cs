using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public abstract class BuildingAbstract : SelectableAbstract, IPoolableObject
{
    public TowerScriptable towerData;
    protected Structs.TowerStruct towerStructData;
    [NaughtyAttributes.ShowNonSerializedField]
    protected bool isPlaced = false;

    protected virtual void OnEnable()
    {
        //Used From Pool
        isPlaced = false;
    }

    [NaughtyAttributes.Button("Placed")]
    public void PlaceToArea()
    {
        if (isPlaced)
        {
            isPlaced = false;
            towerStructData.isPlaced = isPlaced;
            return;
        }

        isPlaced = true;
        towerStructData.isPlaced = isPlaced;
    }

    public bool CanPlaceable()
    {
        return true;
    }

    public void Placed()
    {

    }

    public bool IsPlaced()
    {
        towerStructData.isPlaced = false;
        return false;
    }

    public override void AddToPool()
    {
        //Reset Towers Health
        towerStructData = towerData.GetTowerData();

        base.AddToPool();
    }

    public override Transform UseFromPool()
    {
        base.UseFromPool();

        return this.transform;
    }
}
