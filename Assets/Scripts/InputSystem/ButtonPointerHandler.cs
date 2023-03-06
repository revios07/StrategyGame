using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPointerHandler : MonoBehaviour, IPointerDownHandler
{
    [NaughtyAttributes.ResizableTextArea]
    [SerializeField]
    private bool _isOnGameSelectable;
    private bool _isSoldier;

    [SerializeField]
    private Soldier _soldier;
    [SerializeField]
    private Building _building;

    [ExecuteInEditMode]
    [NaughtyAttributes.Button("Load Referances")]
    private void OnValidate()
    {
        //Check are there any bugs
        if (_soldier != null)
        {
            if (TryGetComponent<Soldier>(out Soldier soldierRef))
            {
                _soldier = soldierRef;
            }

            if (TryGetComponent<Building>(out Building buildingRef))
            {
                Debug.LogError("An Object Cant be Building and Soldier Same Time!");
            }

            _isSoldier = true;
        }
        if (_building != null)
        {
            if (TryGetComponent<Building>(out Building buildingRef))
            {
                _building = buildingRef;

            }

            if (TryGetComponent<Soldier>(out Soldier soldierRef))
            {

                Debug.LogError("An Object Cant be Building and Soldier Same Time!");
            }

            _isSoldier = false;
        }
    }

    //Can Select Units Here
    public void OnPointerDown(PointerEventData eventData)
    {
        //Selected Item On Game Area
        if (_isOnGameSelectable)
        {
            if (_isSoldier)
            {
                _soldier.OnSelectedItemFromGame();
            }
            else if (!_isSoldier)
            {
                _building.OnSelectedItemFromGame();
            }
        }
        //Selected Item On Production Panel
        else if (!_isOnGameSelectable)
        {
            if (_isSoldier)
            {
                _soldier.OnItemSelectedFromMenu();
            }
            else if (!_isSoldier)
            {
                _building.OnItemSelectedFromMenu();
            }
        }
    }

    public void SetDataType(TowerScriptable buildingType)
    {
        if(buildingType.GetTowerData().towerName == "Barracks")
        {

        }
        else if(buildingType.GetTowerData().towerName == "PowerPlant")
        {

        }
    }
}
