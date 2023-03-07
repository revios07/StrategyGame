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
    private int _placementIndex;

    private void Awake()
    {
        _selectables = new Image[transform.childCount];
        for(int i = 0; i < transform.childCount; ++i)
        {
            _selectables[i] = transform.GetChild(i).GetChild(0).GetComponent<Image>();
        }

        //_selectables = GetComponentsInChildren<Image>();
        SetSelectablesSprites();
    }

    [NaughtyAttributes.Button()]
    private void SetSelectablesSprites()
    {
        _placementIndex = 1;
        int j = 0;
        if (_isRightTower)
        {
            TowerScriptable temp = _buildingDatas[0];

            _buildingDatas[0] = _buildingDatas[1];
            _buildingDatas[1] = temp;
        }

        for (int i = 0; i < _selectables.Length; ++i)
        {
            if(i == 0)
            {

            }
            else
            {
                ReverseToOnetoZero(ref j);
                _placementIndex += 1;
            }

            if(_placementIndex % 3f == 0f)
            {
                ReverseToOnetoZero(ref j);
                _placementIndex = 1;
            }

            _selectables[i].sprite = _buildingDatas[j].GetTowerData().towerSprite;
            _selectables[i].GetComponent<ButtonPointerHandler>().SetDataType(_buildingDatas[j].typeOfThisSelectable);
        }
    }

    private void ReverseToOnetoZero(ref int reverseNumber)
    {
        if (reverseNumber == 0)
            reverseNumber = 1;
        else
            reverseNumber = 0;
    }
}
