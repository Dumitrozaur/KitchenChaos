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
    
    public enum Bindings
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        AlternateInteract,
        Pause
    }

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    
    private PlayerGameInput _playerInput;
    private void Awake()
    {
        Instance = this;
        _playerInput = new PlayerGameInput();
        
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            _playerInput.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
        
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

    public string GetBndingText(Bindings bindings)
    {
        switch (bindings)
        {
            case Bindings.MoveUp:
                return _playerInput.Player.Move.bindings[1].ToDisplayString();
            case Bindings.MoveDown:
                return _playerInput.Player.Move.bindings[2].ToDisplayString();
            case Bindings.MoveLeft:
                return _playerInput.Player.Move.bindings[3].ToDisplayString();
            case Bindings.MoveRight:
                return _playerInput.Player.Move.bindings[4].ToDisplayString();
            case Bindings.Interact:
                return _playerInput.Player.Interact.bindings[0].ToDisplayString();
            case Bindings.AlternateInteract:
                return _playerInput.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Bindings.Pause:
                return _playerInput.Player.Pause.bindings[0].ToDisplayString();
            default:
                return "Key Not Found!";
        } 
    }

    public void RebindBinding(Bindings binding, Action onActionRebound)
    {
        _playerInput.Player.Disable();

        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            case Bindings.MoveUp:
                default:
                inputAction = _playerInput.Player.Move;
                bindingIndex = 1;
                break;
            case Bindings.MoveDown:
                inputAction = _playerInput.Player.Move;
                bindingIndex = 2;
                break;
            case Bindings.MoveLeft:
                inputAction = _playerInput.Player.Move;
                bindingIndex = 3;
                break;
            case Bindings.MoveRight:
                inputAction = _playerInput.Player.Move;
                bindingIndex = 4;
                break;
            case Bindings.Interact:
                inputAction = _playerInput.Player.Interact;
                bindingIndex = 0;
                break;
            case Bindings.AlternateInteract:
                inputAction = _playerInput.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Bindings.Pause:
                inputAction = _playerInput.Player.Pause;
                bindingIndex = 0;
                break;
            
        }
        
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
            {
                _playerInput.Player.Enable();
                onActionRebound();
                
                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, _playerInput.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();
        
    }
}
