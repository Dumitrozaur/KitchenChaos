using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IKitchenObjectParent, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private KitchenObject _kitchenObject;


    [SerializeField] private FryingRecipesSO[] fryingRecipesSoArray;
    [SerializeField] private BurningRecipeSO[] burningRecipesSoArray;

    private State state;
    private float fryingTimer;
    private FryingRecipesSO fryingRecipesSo;
    private float buriningTimer;
    private BurningRecipeSO burningRecipeSo;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = fryingTimer / fryingRecipesSo.fryingTimerMax
                    });
                    
                    if (fryingTimer > fryingRecipesSo.fryingTimerMax)
                    {
                        //Fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipesSo.output, this);

                        state = State.Fried;
                        buriningTimer = 0f;
                        burningRecipeSo = GetBurningRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            state = state
                        });
                        
                        
                    }
                    break;
                case State.Fried:
                    buriningTimer += Time.deltaTime;
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = fryingTimer / fryingRecipesSo.fryingTimerMax
                    });
                    
                    if (buriningTimer > burningRecipeSo.burningTimerMax)
                    {
                        //Fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSo.output, this);

                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            state = state
                        });
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    
                    break;
            }
        
        }
        Debug.Log("State" + state);
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    fryingRecipesSo = GetFryingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());

                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = fryingTimer / fryingRecipesSo.fryingTimerMax
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = 0f
                    });
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                {
                    state = state
                });
            }
        }
    }
    

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        FryingRecipesSO fryingRecipesSo = GetFryingRecipeSoWithInput(inputKitchenObjectSo);
        return fryingRecipesSo != null;
    }
    
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObject)
    {
        foreach (FryingRecipesSO fryingRecipesSo  in fryingRecipesSoArray)
        {
            if (fryingRecipesSo.input == kitchenObject)
            {
                return fryingRecipesSo.output;
            }
        }

        return null; 
    }

    private FryingRecipesSO GetFryingRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (FryingRecipesSO fryingRecipesSo in fryingRecipesSoArray)
        {
            if (fryingRecipesSo.input == inputKitchenObjectSo)
            {
                return fryingRecipesSo;
            }
        }

        return null;
    }
    
    private BurningRecipeSO GetBurningRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (BurningRecipeSO burningRecipeSo in burningRecipesSoArray)
        {
            if (burningRecipeSo.input == inputKitchenObjectSo)
            {
                return burningRecipeSo;
            }
        }

        return null;
    }
    

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
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
