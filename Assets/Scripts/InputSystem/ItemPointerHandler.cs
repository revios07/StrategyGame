using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPointerHandler : MonoBehaviour
{
    [NaughtyAttributes.ShowNonSerializedField]
    private bool _isSoldier;

    private Building building;
    private Soldier soldier;

    private void Awake()
    {
        TryGetComponent<Building>(out building);
        TryGetComponent<Soldier>(out soldier);

        if (soldier != null)
            _isSoldier = true;
        else if (building != null)
            _isSoldier = false;
        else
            Debug.LogError("This Can be A Type of Selectable!");
    }

    public void OnMouseDown()
    {
        if (_isSoldier)
        {
            soldier.OnSelectedItemFromGame();
        }
        //Building Selected
        else if (!_isSoldier)
        {
            building.OnSelectedItemFromGame();
        }
    }
}
