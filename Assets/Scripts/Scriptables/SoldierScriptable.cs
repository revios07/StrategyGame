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
    [ShowNonSerializedField]
    private Vector2 _sizeOfSoldier = Vector2.one;
    public ObjectType typeOfThis;

    public Structs.SoldierStruct GetSoldierData()
    {
        return new Structs.SoldierStruct(_soldierSprite, _soldierIndex, _soldierHealth, _soldierDamage);
    }
}
