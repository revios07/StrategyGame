using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : SoldierAbstract
{
    private void Start()
    {
        //Give Random Health To Soldier
        //Load Soldier Data
        soldierData = GetRandomSoldier();
    }

    public override void OnItemSelectedFromMenu()
    {
        EventManager.onSoldierSelectedInProductionPanel?.Invoke(ref base.soldierData);

        base.OnItemSelectedFromMenu();
    }

    public override void OnSelectedItemFromGame()
    {
        EventManager.onSoldierSelectedInProductionPanel?.Invoke(ref base.soldierData);

        base.OnSelectedItemFromGame();
    }
}
