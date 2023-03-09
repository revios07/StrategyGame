using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public abstract class BuildingAbstract : SelectableAbstract, IPoolableObject, ICanTakeDamagePlayableObject
{
    public TowerScriptable towerData;
    protected Structs.TowerStruct towerStructData;
    [SerializeField]
    protected BoundsInt spawnPoint;

    protected virtual void OnEnable()
    {
        if(towerData.GetTowerData().objectType == Enums.ObjectType.Barracks)
        {
            //Spawn Point Area
            spawnPoint.position = new Vector3Int(0, -10, 0);
            sizeArea.size += spawnPoint.size;
            sizeArea.position += spawnPoint.position;
        }
        //Used From Pool
        isPlaced = false;
    }

    [NaughtyAttributes.Button("Placed")]
    public override void PlaceToArea()
    {
        base.PlaceToArea();

        if (isPlaced)
        {
            isPlaced = false;
            towerStructData.isPlaced = isPlaced;
            return;
        }

        isPlaced = true;
        towerStructData.isPlaced = isPlaced;
    }

    #region Soldier Spawn
    public void SpawnUnits()
    {


    }
    #endregion

    //Interface Implementations
    #region Attack and Take Damage
    public override void TakeDamage(int damage)
    {
        towerStructData.towerHealth -= damage;
        if(damage <= 0)
        {
            towerStructData.towerHealth = 0;
            base.isDead = true;

            //Tower Destroyed Here
            //Add Pool Again GameObject
        }

        base.TakeDamage(damage);
        SetSliderValue(towerStructData.towerHealth);
    }
    #endregion

    #region Pool Calls
    public override void AddToPool()
    {
        //Reset Data
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
