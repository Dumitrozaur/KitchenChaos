using System;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour, IKitchenObjectParent
{
    public static Player LocalInstance { get; private set; }
    public static EventHandler OnAnyPlayerSpawned;
    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
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

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (LocalInstance == null)
        {
            LocalInstance = this;
        }
        
        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);

        if (IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnect;
        }
    }

    private void NetworkManager_OnClientDisconnect(ulong clientId)
    {
        if (clientId == OwnerClientId && HasKitchenObject())
        {
            KitchenObject.DestroyKitchenObject(GetKitchenObject());
        }
    }

    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
        
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if(GameManager1.Instance.IsGamePlaying() == false) return;
        
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }
    
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {   
        if(GameManager1.Instance.IsGamePlaying() == false) return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        
        HandleMovement();
        HandleInteraction();
        
    }

    public bool Walking()
    {
        return !isWalking;
    }
    
    private void HandleInteraction()
    {
        Vector2 input = GameInput.Instance.GetMovementVectorNormalized();
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
    }
    
    private void HandleMovement()
    {
        Vector2 input = GameInput.Instance.GetMovementVectorNormalized();
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
    public new static void ResetStaticData()
    {
        OnAnyPlayerSpawned = null;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }

    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }
}
