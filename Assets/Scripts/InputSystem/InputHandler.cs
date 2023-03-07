using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private InputData _inputData;
    public Vector2 _mousePosition { get; private set; }

    private void Awake()
    {
        _inputData.ResetInputData();
    }

    private void Update()
    {
        _mousePosition = CheckMousePosition();
        _inputData.SetInputData(_mousePosition);
    }

    private void LateUpdate()
    {
        //Check Is Clicking Here
    }

    private Vector2 CheckMousePosition()
    {
        return Input.mousePosition;
    }
}
