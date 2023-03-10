using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Enums;
using UnityEngine.Tilemaps;

[Tooltip("This Class Only Works With UI")]
[RequireComponent(typeof(RectTransform))]
public class ButtonPointerHandler : MonoBehaviour, IPointerDownHandler
{
    public ObjectType typeOfSelectable;

    [NaughtyAttributes.ResizableTextArea]
    [SerializeField]
    private bool _isOnGameSelectable, _isSoldier, _isSoldierSpawner;

    [SerializeField]
    private Soldier _soldier;
    [SerializeField]
    private Building _building;
    [SerializeField]
    private SoldierScriptable _soldierData;
    [SerializeField]
    private TowerScriptable _barracksData, _powerPlantData;

    private void OnEnable()
    {

    }

    private void OnDisable()
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

        if (_isSoldier)
            _isSoldier = false;
    }

    //Can Select Units Here
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isSoldierSpawner)
        {
            if (GridPlacementSystem.instance.CanSpawnSoldier(GamePlayController.lastSelectedBuilding))
            {
                GridPlacementSystem.instance.SpawnSoldier();
            }
            return;
        }

        /*
        if (_isSoldierSpawner)
        {
            EventManager.onSoldierSpawnedRequest?.Invoke();
            EventManager.pickRequestFromPool?.Invoke(ObjectType.Soldier);

            return;
        }*/

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
    }

    /*
    public Structs.TowerStruct BarracksSelects(ref Structs.TowerStruct towerStruct)
    {
        bool canSpawnSoldier = false;
        BoundsInt spawnArea = towerStruct.size;

        if(towerStruct.objectType == ObjectType.Barracks)
        {
            canSpawnSoldier = GridPlacementSystem.instance.CanSpawnSoldier(GamePlayController.lastSelectedBuilding);
        }

        //Spawn Soldier At Empty Spawn Area
        if (canSpawnSoldier)
        {
            GridPlacementSystem.instance.SpawnSoldier();
        }

        return towerStruct;
    }
    */

    public void SetDataType(Enums.ObjectType typeOfSelectable)
    {
        _isOnGameSelectable = false;
        this.typeOfSelectable = typeOfSelectable;
    }
}
