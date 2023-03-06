using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoldierAbstract : SelectableAbstract
{
    [SerializeField]
    protected SoldierScriptable soldierScriptable;
    public int soldierHealth;
}
