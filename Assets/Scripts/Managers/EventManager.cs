using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;

public class EventManager : MonoBehaviour
{
    #region Game Base Events
    public delegate void OnGameStarted();
    public delegate void OnGameEnded(bool isWin);
    #endregion

    public delegate Vector2 OnGameBoardClicked(Vector2 mousePosition);

    #region UI Events MVC
    public delegate Structs.SoldierStruct OnSoldierSelectedInProductionPanel(ref Structs.SoldierStruct selectedSoldierData);
    public delegate void OnSoldierSpawnedRequest();
    public delegate Structs.TowerStruct OnTowerSelectedInProductionPanel(ref Structs.TowerStruct selectedTowerData);

    public delegate Structs.SoldierStruct OnSoldierSelectedGameBoard(ref Structs.SoldierStruct selectedSoliderData);
    public delegate Structs.TowerStruct OnTowerScriptableSelectedGameBoard(ref Structs.TowerStruct selectedTowerData);

    public static OnTowerSelectedInProductionPanel onTowerSelectedInProductionPanel;
    public static OnSoldierSpawnedRequest onSoldierSpawnedRequest;
    public static OnSoldierSelectedInProductionPanel onSoldierSelectedInProductionPanel;

    public static OnTowerScriptableSelectedGameBoard onTowerScriptableSelectedGameBoard;
    public static OnSoldierSelectedGameBoard onSoldierSelectedGameBoard;
    #endregion

    #region Object Controller
    public delegate Vector2 OnTowerPlacement();
    public delegate Vector2 OnSoldierSpawned();

    public delegate Transform PickRequestFromPool(Enums.ObjectType typeOfPooledObject);
    public delegate void PickedFromPool(Enums.ObjectType pickedObjectType, Transform pickedTransform);
    public delegate void OnbObjectAddToPool(Enums.ObjectType pooledObjectType, Transform pooledObject);

    public static OnbObjectAddToPool onObjectAddToPool;
    public static PickRequestFromPool pickRequestFromPool;
    public static PickedFromPool pickedFromPool;
    #endregion
}
