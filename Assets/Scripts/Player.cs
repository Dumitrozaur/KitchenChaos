using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private int MovSpeed = 1;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float _playerRadius = .7f;
    [SerializeField] private float _playerHeight = 2f;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private LayerMask _layerMask;
    public bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject _kitchenObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is more than one player instance!!!!!!!!!!!!!!!");
        }
    }

    private void Start()
    {
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
        _gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }
    
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool Walking()
    {
        return !isWalking;
    }
    
    
    private void HandleInteraction()
    {
        Vector2 input = _gameInput.GetMovementVectorNormalized();
        Vector3 movDir = new Vector3(input.x, 0f, input.y);
        
        if (movDir != Vector3.zero)
        {
            lastInteractDir = movDir;
        }
        
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, _layerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                SetSelectedCounter(baseCounter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
        Debug.Log(selectedCounter);
    }
    
    private void HandleMovement()
    {
        Vector2 input = _gameInput.GetMovementVectorNormalized();
        Vector3 movDir = new Vector3(input.x, 0f, input.y);
        
        var moveDistance = MovSpeed * Time.deltaTime;
        var step = movDir * moveDistance;
        
        bool canMove =!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, movDir, moveDistance);
        
        isWalking = movDir != Vector3.zero;
        if (!canMove)
        {
            var moveDirX = new Vector3(movDir.x, 0, 0); 
            bool canMoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, moveDirX, moveDistance);
            if (canMoveX)
            {
                canMove = true;
                step = moveDirX * moveDistance;
            }

            if (!canMoveX)
            {
                var moveDirZ = new Vector3(0, 0, moveDirX.y);
                bool canMoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, moveDirZ, moveDistance);
                if (canMoveZ)
                {
                    canMove = true;
                    step = moveDirZ * moveDistance;
                }
            }
        }
        if (canMove)
        {
            transform.position += step;
        }

        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter newSelectedCounter)
    {
        selectedCounter = newSelectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs()
        {
            selectedCounter = newSelectedCounter
        });
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;

        if (_kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public bool TryGetPlateObject(out PlateKitchenObject plateKitchenObject)
    {
        if (_kitchenObject.TryGetComponent(out PlateKitchenObject plate))
        {
            plateKitchenObject = plate;
            return true;
        }

        plateKitchenObject = null;
        return false;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
