using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFactory : GameObjectFactory<Soldier>
{
    [SerializeField]
    private SoldierScriptable[] _soldierDatas;
    [SerializeField]
    private GameObject[] _soldierPrefabs;
}
