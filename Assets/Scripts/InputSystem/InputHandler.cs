using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private InputData _inputData;
    private Vector2 _mousePosition;

    private void Update()
    {
        _mousePosition = CheckMousePosition();
        _inputData.SetInputData(_mousePosition);
    }

    private void LateUpdate()
    {
        
    }

    private Vector2 CheckMousePosition()
    {
        return Input.mousePosition;
    }
}
