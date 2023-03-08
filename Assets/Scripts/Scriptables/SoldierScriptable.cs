using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Enemy/SoldierUnit", fileName = "SoldierUnit")]
public class SoldierScriptable : ScriptableObject
{
    [SerializeField]
    private Sprite _soldierSprite;
    [SerializeField]
    private int _soldierIndex, _soldierHealth, _soldierDamage;
    [SerializeField]
    private BoundsInt _sizeOfSoldier;
    public Enums.ObjectType typeOfThis;

    public Structs.SoldierStruct GetSoldierData()
    {
        return new Structs.SoldierStruct(_soldierSprite, _soldierIndex, _soldierHealth, _soldierDamage, typeOfThis, _sizeOfSoldier);
    }
}
