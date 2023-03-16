using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public abstract class BuildingAbstract : SelectableAbstract, IPoolableObject, ICanTakeDamagePlayableObject
{
    public int towerHealth => towerStructData.towerHealth;
    public TowerDataSO towerData;
    protected BoundsInt spawnPoint;
    protected Structs.TowerStruct towerStructData;

    #region Pool Calls
    public override void AddToPool()
    {
        base.AddToPool();
    }
    public override Transform UseFromPool()
    {
        //Reset Data
        towerStructData = towerData.GetTowerData();
        SetMaxValueOfSlide(towerStructData.towerHealth);
        SetSliderValue(towerStructData.towerHealth);

        base.UseFromPool();

        return this.transform;
    }
    #endregion

    #region Soldier Spawn
    public void SpawnUnits()
    {


    }
    #endregion

    #region Placement to GameBoard
    public override void PlaceToArea()
    {
        base.PlaceToArea();

        isPlaced = true;
        towerStructData.isPlaced = isPlaced;
    }
    #endregion

    #region Damage/Health
    public override void TakeDamage(int damage)
    {
        if (base.isDead)
            return;

        towerStructData.towerHealth -= damage;
        if (towerStructData.towerHealth <= 0)
        {
            towerStructData.towerHealth = 0;
            base.isDead = true;
            AddToPool();
            //Tower Destroyed Here
            //Add Pool Again GameObject
        }

        EventManager.onSelectableTakeDamageInGame?.Invoke(this);
        base.TakeDamage(damage);
        healthTextUpdater.WriteHealth(towerStructData.towerHealth);
        SetSliderValue(towerStructData.towerHealth);
    }
    #endregion
}
