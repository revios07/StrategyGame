using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : BuildingAbstract
{
    void Start()
    {
        //Load TowerData
        towerStructData = towerData.GetTowerData();
    }

    void Update()
    {

    }

    public override void OnItemSelectedFromMenu()
    {
        EventManager.onTowerSelectedInProductionPanel?.Invoke(ref towerStructData);

        base.OnItemSelectedFromMenu();
    }

    public override void OnSelectedItemFromGame()
    {
        if (!isPlaced)
            return;

        EventManager.onTowerScriptableSelectedGameBoard?.Invoke(ref towerStructData);

        base.OnSelectedItemFromGame();
    }
}
