using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Input/InputData", fileName = "InputData")]
public class InputData : ScriptableObject
{
    [NaughtyAttributes.ShowNonSerializedField]
    private float _xInput, _yInput;
    [NaughtyAttributes.ShowNonSerializedField]
    private Vector2 _mousePosition;


    private void OnEnable()
    {
        ResetInputData();
    }

    private void ResetInputData()
    {
        _xInput = 0f;
        _yInput = 0f;
        _mousePosition = Vector2.zero;
    }

    public Vector2 GetMousePosition()
    {
        return _mousePosition;
    }

    public void SetInputData(Vector2 mousePosition)
    {
        _xInput = mousePosition.x;
        _yInput = mousePosition.y;
        this._mousePosition = mousePosition;
    }
}
