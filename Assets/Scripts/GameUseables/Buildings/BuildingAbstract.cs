using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public abstract class BuildingAbstract : SelectableAbstract, IPoolableObject, ICanTakeDamagePlayableObject
{
    public TowerScriptable towerData;
    protected Structs.TowerStruct towerStructData;

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

    //Interface Implementations
    #region Attack and Take Damage
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        towerStructData.towerHealth -= damage;
        if(damage <= 0)
        {
            towerStructData.towerHealth = 0;

            //Tower Destroyed Here
            //Add Pool Again GameObject
        }

        SetSliderValue(towerStructData.towerHealth);
    }
    #endregion

    #region Pool Calls
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
    #endregion
}
