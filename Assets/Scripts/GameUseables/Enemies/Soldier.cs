using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : SoldierAbstract
{
    public override void OnItemSelectedFromMenu()
    {
        EventManager.onSoldierSelectedInProductionPanel?.Invoke(base.soldierScriptable);

        base.OnItemSelectedFromMenu();
    }

    public override void OnSelectedItemFromGame()
    {
        EventManager.onSoldierSelectedInProductionPanel?.Invoke(base.soldierScriptable);

        base.OnSelectedItemFromGame();
    }
}
