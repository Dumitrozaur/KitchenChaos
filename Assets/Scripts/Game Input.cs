using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerGameInput _playerInput;
    private void Awake()
    {
        _playerInput = new PlayerGameInput();
        _playerInput.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInput.Player.Move.ReadValue<Vector2>();
        

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
