using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : SoldierAbstract
{
    protected override void Start()
    {
        base.Start();
        //Give Random Health To Soldier
        //Load Soldier Data
        soldierStructData = GetRandomSoldier();
        SetMaxValueOfSlide(soldierStructData.soldierHealth);
        SetSliderValue(soldierStructData.soldierHealth);
    }

    public override void OnItemSelectedFromMenu()
    {
        EventManager.onSoldierSelectedInProductionPanel?.Invoke(ref base.soldierStructData);

        base.OnItemSelectedFromMenu();
    }

    public override void OnSelectedItemFromGame()
    {
        EventManager.onSoldierSelectedInProductionPanel?.Invoke(ref base.soldierStructData);

        base.OnSelectedItemFromGame();
    }
}
