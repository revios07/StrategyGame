using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : GameObjectFactory<Building>
{
    [SerializeField]
    private TowerScriptable[] _buildingDatas;
    [SerializeField]
    private GameObject[] _buildingPrefabs;
}
