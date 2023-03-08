using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Tower", fileName = "Tower")]
public class TowerScriptable : ScriptableObject
{
    [SerializeField]
    private Sprite _towerSprite;
    [SerializeField]
    private string _towerName;
    [SerializeField]
    private int _towerIndex, _towerHealth, _towerDamage;
    [SerializeField]
    private BoundsInt _sizeOfTower;
    public Enums.ObjectType typeOfThisSelectable;

    public Structs.TowerStruct GetTowerData()
    {
        return new Structs.TowerStruct(_towerSprite, _towerName, _towerIndex, _towerHealth, _towerDamage, _sizeOfTower, typeOfThisSelectable);
    }
}