using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Tooltip("Only Use For In Game Placed Items")]
[RequireComponent(typeof(SelectableAbstract))]
public class ItemPointerHandler : MonoBehaviour
{
    [NaughtyAttributes.ShowNonSerializedField]
    private bool _isSoldier;

    private Building _building;
    private Soldier _soldier;

    private void Awake()
    {
        TryGetComponent<Building>(out _building);
        TryGetComponent<Soldier>(out _soldier);

        if (_soldier != null)
            _isSoldier = true;
        else if (_building != null)
            _isSoldier = false;
        else
            Debug.LogError("This Can be A Type of Selectable!");
    }

    public void OnMouseDown()
    {
        if (_isSoldier)
        {
            _soldier.OnSelectedItemFromGame();
        }
        //Building Selected
        else if (!_isSoldier)
        {
            _building.OnSelectedItemFromGame();
        }
    }
}
