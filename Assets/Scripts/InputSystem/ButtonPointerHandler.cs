using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Enums;

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

    [SerializeField]
    private SoldierScriptable _soldierData;
    [SerializeField]
    private TowerScriptable _barracksData, _powerPlantData;

    public ObjectType typeOfSelectable;

    [SerializeField]
    private bool _isSoldierSpawner;

    private void Start()
    {
        
    }

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
        if (_isSoldierSpawner)
        {
            EventManager.onSoldierSpawnedRequest?.Invoke();

            return;
        }

        //Pick Item From Buy Area
        if (!_isOnGameSelectable)
        {
            if (typeOfSelectable == ObjectType.Barracks)
            {
                Structs.TowerStruct towerData = _barracksData.GetTowerData();

                EventManager.onTowerSelectedInProductionPanel?.Invoke(ref towerData);
                EventManager.pickRequestFromPool?.Invoke(ObjectType.Barracks);
            }
            else if (typeOfSelectable == ObjectType.PowerPlant)
            {
                Structs.TowerStruct barracksData = _powerPlantData.GetTowerData();

                EventManager.onTowerSelectedInProductionPanel?.Invoke(ref barracksData);
                EventManager.pickRequestFromPool?.Invoke(ObjectType.PowerPlant);
            }
        }
        else if (_isOnGameSelectable)
        {

        }


        return;
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

    public void SetDataType(Enums.ObjectType typeOfSelectable)
    {
        _isOnGameSelectable = false;
        this.typeOfSelectable = typeOfSelectable;
    }
}
