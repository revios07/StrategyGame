using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Enemy/SoldierUnit", fileName = "SoldierUnit")]
public class SoliderScriptable : ScriptableObject
{
    [SerializeField]
    private Sprite _soldierSprite;
    [SerializeField]
    private int _soldierIndex, _soldierHealth, _soldierDamage;
    [ShowNonSerializedField]
    private Vector2 _sizeOfSoldier = Vector2.one;
}
