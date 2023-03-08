using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : GameObjectFactory<Building>
{
    [SerializeField]
    private TowerScriptable[] _buildingDatas;
    [SerializeField]
    private GameObject[] _buildingPrefabs;

    public override Building GetNewInstance(string gameObjectType)
    {
        for (int i = 0; i < _buildingDatas.Length; ++i)
        {
            if (gameObjectType == _buildingDatas[i].GetTowerData().towerName)
            {
                for (int j = 0; j < _buildingPrefabs.Length; ++j)
                {
                    if (_buildingPrefabs[j].GetComponent<Building>().towerData.GetTowerData().towerName == gameObjectType)
                    {
                        return Instantiate(_buildingPrefabs[j]).GetComponent<Building>();
                    }
                }
            }
        }

        Debug.LogError("Cant Find Type!");
        return null;
    }
}
