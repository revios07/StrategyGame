using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Input/InputData", fileName = "InputData")]
public class InputDataSO : ScriptableObject
{
    [NaughtyAttributes.ShowNonSerializedField]
    private float _xInput, _yInput;
    [NaughtyAttributes.ShowNonSerializedField]
    private Vector2 _mousePosition;
    [Range(1f, 10f)]
    [SerializeField]
    private float _scrollSpeedMultiplier;
    private float _scrollSpeed;

    private void OnValidate()
    {
        _scrollSpeed = _scrollSpeedMultiplier * 10000f;
    }

    public void ResetInputData()
    {
        _xInput = 0f;
        _yInput = 0f;
        _mousePosition = Vector2.zero;
    }

    public Vector2 GetMousePosition()
    {
        return _mousePosition;
    }

    public float GetScrollSpeed()
    {
        if(_scrollSpeed < 100f)
        {
            _scrollSpeed = _scrollSpeedMultiplier * 10000f;
        }

        return _scrollSpeed;
    }

    public void SetInputData(Vector2 mousePosition)
    {
        _xInput = mousePosition.x;
        _yInput = mousePosition.y;
        this._mousePosition = mousePosition;
    }
}
