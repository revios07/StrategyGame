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
    public delegate SoliderScriptable OnSoldierSelectedInProductionPanel(SoliderScriptable selectedSoldierData);
    public delegate TowerScriptable OnTowerSelectedInProductionPanel(TowerScriptable selectedTowerData);
    #endregion
}
