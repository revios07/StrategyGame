using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingAbstract : SelectableAbstract
{
    public TowerScriptable towerData;
    public bool isPlaced = false;

    protected virtual void OnEnable()
    {
        //Used From Pool
        isPlaced = false;
    }

    public void PlaceToArea()
    {
        isPlaced = true;
    }
}
