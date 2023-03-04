using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Game Base Events
    public delegate void OnGameStarted();
    public delegate void OnGameEnded(bool isWin);
    #endregion

    public delegate Vector2 OnGameBoardClicked(Vector2 mousePosition);

    #region UI Events MVC
    public delegate SoldierScriptable OnSoldierSelectedInProductionPanel(SoldierScriptable selectedSoldierData);
    public delegate TowerScriptable OnTowerSelectedInProductionPanel(TowerScriptable selectedTowerData);

    public delegate SoldierScriptable OnSoldierSelectedGameBoard(SoldierScriptable selectedSoliderData);
    public delegate TowerScriptable OnTowerScriptableSelectedGameBoard(TowerScriptable selectedTowerData);

    public static OnTowerSelectedInProductionPanel onTowerSelectedInProductionPanel;
    public static OnSoldierSelectedInProductionPanel onSoldierSelectedInProductionPanel;

    public static OnTowerScriptableSelectedGameBoard onTowerScriptableSelectedGameBoard;
    public static OnSoldierSelectedGameBoard onSoldierSelectedGameBoard;
    #endregion

    #region Object Controller
    public delegate Vector2 OnTowerPlacement();
    public delegate Vector2 OnSoldierSpawned();
    #endregion
}
