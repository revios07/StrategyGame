using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProductionPanel : MonoBehaviour
{
    [SerializeField]
    private Image[] _selectables;

    [SerializeField]
    private TowerScriptable[] _buildingDatas;
    [SerializeField]
    private bool _isRightTower;

    private void Awake()
    {
        _selectables = GetComponentsInChildren<Image>();
        SetSelectablesSprites();
    }

    [NaughtyAttributes.Button()]
    private void SetSelectablesSprites()
    {
        int j = 0;
        if (_isRightTower)
        {
            TowerScriptable temp = _buildingDatas[0];

            _buildingDatas[0] = _buildingDatas[1];
            _buildingDatas[1] = temp;
        }

        for (int i = 0; i < _selectables.Length; ++i)
        {
            _selectables[i].sprite = _buildingDatas[j].GetTowerData().towerSprite;
            _selectables[i].GetComponent<ButtonPointerHandler>().SetDataType(_buildingDatas[j]);

            if (j == 0)
                j = 1;
            else
                j = 0;
        }
    }
}
