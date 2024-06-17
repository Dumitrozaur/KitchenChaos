using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using Debug = System.Diagnostics.Debug;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    
    private PlayerGameInput _playerInput;
    private void Awake()
    {
        Instance = this;
        _playerInput = new PlayerGameInput();
        _playerInput.Player.Enable();

        _playerInput.Player.Interact.performed += Interact_performed;
        _playerInput.Player.InteractAlternate.performed += InteractAlternate_performed;
        _playerInput.Player.Pause.performed += Pause_Performed;
    }

    private void OnDestroy()
    {
        _playerInput.Player.Interact.performed -= Interact_performed;
        _playerInput.Player.InteractAlternate.performed -= InteractAlternate_performed;
        _playerInput.Player.Pause.performed -= Pause_Performed;
        
        _playerInput.Dispose();
    }

    private void Pause_Performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractAction != null)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInput.Player.Move.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
