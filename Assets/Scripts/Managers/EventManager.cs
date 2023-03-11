using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;

public class EventManager : MonoBehaviour
{
    #region Game Base Events Not Need
    public delegate void OnGameStarted();
    public delegate void OnGameEnded(bool isWin);
    #endregion

    public delegate Vector2 OnGameBoardClicked(Vector2 mousePosition);

    #region UI Events MVC
    //Delegates
    public delegate Structs.SoldierStruct OnSoldierSelectedInProductionPanel(ref Structs.SoldierStruct selectedSoldierData, SelectableAbstract selectableAbstract);
    public delegate void OnSoldierSpawnedRequest();
    public delegate Structs.TowerStruct OnTowerSelectedInProductionPanel(ref Structs.TowerStruct selectedTowerData);

    public delegate Structs.SoldierStruct OnSoldierSelectedGameBoard(ref Structs.SoldierStruct selectedSoliderData, SelectableAbstract selectableAbstract);
    public delegate Structs.TowerStruct OnTowerScriptableSelectedGameBoard(ref Structs.TowerStruct selectedTowerData, SelectableAbstract selectableAbstract);

    //Calls
    public static OnTowerSelectedInProductionPanel onTowerSelectedInProductionPanel;
    public static OnSoldierSpawnedRequest onSoldierSpawnedRequest;
    public static OnSoldierSelectedInProductionPanel onSoldierSelectedInProductionPanel;

    public static OnTowerScriptableSelectedGameBoard onTowerScriptableSelectedGameBoard;
    public static OnSoldierSelectedGameBoard onSoldierSelectedGameBoard;
    #endregion

    #region Object Controller
    //Delegates
    public delegate Vector2 OnTowerPlacement();
    public delegate Vector2 OnSoldierSpawned();
    public delegate void OnSelectableTakeDamageInGame(SelectableAbstract selectableAbstract);

    public delegate Transform PickRequestFromPool(Enums.ObjectType typeOfPooledObject);
    public delegate void PickedFromPool(Enums.ObjectType pickedObjectType, Transform pickedTransform);
    public delegate void OnbObjectAddToPool(Enums.ObjectType pooledObjectType, Transform pooledObject);

    //Calls
    public static OnSelectableTakeDamageInGame onSelectableTakeDamageInGame;

    public static OnbObjectAddToPool onObjectAddToPool;
    public static PickRequestFromPool pickRequestFromPool;
    public static PickedFromPool pickedFromPool;
    #endregion
}
