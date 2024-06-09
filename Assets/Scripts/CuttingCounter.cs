using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs: EventArgs
    {
        public float progressNormalized;
    }
    
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private KitchenObject _kitchenObject;

    [SerializeField] private CuttingRecipies[] cuttingRecipies;

    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipies cuttingRecipeSo = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSo());

                    OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs
                    {
                        progressNormalized =  (float)cuttingProgress / cuttingRecipeSo.cuttingMAX
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
            }
        }
    }


    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSo()))
        {
            cuttingProgress++;
            
            
            CuttingRecipies cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSo());

            
            
            OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs
            {
                progressNormalized =  (float)cuttingProgress / cuttingRecipeSO.cuttingMAX
            });
            
            
            
            
            
            if (cuttingProgress >= cuttingRecipeSO.cuttingMAX)
            {
            
                KitchenObjectSO outputKitchenObjcetSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSo());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjcetSo, this);
            }
            
        }
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        CuttingRecipies cuttingRecipieSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSo);
        return cuttingRecipieSO != null;
    }
    
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObject)
    {
        foreach (CuttingRecipies cuttingRecipiesSO  in cuttingRecipies)
        {
            if (cuttingRecipiesSO.input == kitchenObject) ;
            return cuttingRecipiesSO.output;
        }

        return null; 
    }

    private CuttingRecipies GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipies cuttingRecipeSo in cuttingRecipies)
        {
            if (cuttingRecipeSo.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSo;
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
