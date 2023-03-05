using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : BuildingAbstract
{
    public override void OnItemSelectedFromMenu()
    {
        EventManager.onTowerSelectedInProductionPanel?.Invoke(base.towerData);

        base.OnItemSelectedFromMenu();
    }

    public override void OnSelectedItemFromGame()
    {
        EventManager.onTowerScriptableSelectedGameBoard?.Invoke(base.towerData);

        base.OnSelectedItemFromGame();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
