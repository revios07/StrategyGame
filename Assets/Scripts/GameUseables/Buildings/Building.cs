using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : BuildingAbstract
{
    private bool _barracksPositionUpdated;

    protected void Start()
    {
        //Load TowerData
        towerStructData = towerData.GetTowerData();
        SetMaxValueOfSlide(towerStructData.towerHealth);
        SetSliderValue(towerStructData.towerHealth);

        if (towerData.GetTowerData().objectType == Enums.ObjectType.Barracks && !_barracksPositionUpdated)
        {
            //Spawn Point Area
            spawnPoint.position = new Vector3Int(0, -10, 0);
            sizeArea.size += spawnPoint.size;
            sizeArea.position += spawnPoint.position;
            _barracksPositionUpdated = true;
        }
    }
    public override void OnItemSelectedFromMenu()
    {
        EventManager.onTowerSelectedInProductionPanel?.Invoke(ref towerStructData);
        base.OnItemSelectedFromMenu();
    }
    public override void OnSelectedItemFromGame()
    {
        if (!isPlaced || GamePlayController.isCarrying)
            return;

        GamePlayController.lastSelectedBuilding = this;
        EventManager.onTowerScriptableSelectedGameBoard?.Invoke(ref towerStructData, this as SelectableAbstract);
        base.OnSelectedItemFromGame();
    }
}
