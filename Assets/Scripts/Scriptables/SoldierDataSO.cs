using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/SoldierUnit", fileName = "SoldierUnit")]
public class SoldierDataSO : ScriptableObject
{
    public Enums.ObjectType typeOfThis;
    [SerializeField]
    private Sprite _soldierSprite;
    [SerializeField]
    private int _soldierIndex, _soldierHealth, _soldierDamage;
    [SerializeField]
    private BoundsInt _sizeOfSoldier;

    public Structs.SoldierStruct GetSoldierData()
    {
        return new Structs.SoldierStruct(_soldierSprite, _soldierIndex, _soldierHealth, _soldierDamage, typeOfThis, _sizeOfSoldier);
    }
}
