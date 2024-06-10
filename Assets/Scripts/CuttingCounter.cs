using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs: EventArgs
    {
        public float ProgressNormalized;
    }
    
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private KitchenObject _kitchenObject;

    [SerializeField] private CuttingRecipies[] cuttingRecipies;

    public int cuttingProgress;
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
                    CuttingRecipies cuttingRecipeSo = GetcuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());

                    OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs
                    {
                        ProgressNormalized =  (float)cuttingProgress / cuttingRecipeSo.cuttingMAX
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
            
            
            CuttingRecipies cuttingRecipeSo = GetcuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());

            
            
            OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs
            {
                ProgressNormalized =  (float)cuttingProgress / cuttingRecipeSo.cuttingMAX
            });
            
            
            if (cuttingProgress >= cuttingRecipeSo.cuttingMAX)
            {
            
                KitchenObjectSO outputKitchenObjcetSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSo());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjcetSo, this);
            }
            
        }
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        CuttingRecipies cuttingRecipieSo = GetcuttingRecipeSoWithInput(inputKitchenObjectSo);
        return cuttingRecipieSo != null;
    }
    
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObject)
    {
        foreach (CuttingRecipies cuttingRecipiesSo  in cuttingRecipies)
        {
            if (cuttingRecipiesSo.input == kitchenObject)
            {
                return cuttingRecipiesSo.output;
            }
        }

        return null; 
    }

    private CuttingRecipies GetcuttingRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (CuttingRecipies cuttingRecipeSo in cuttingRecipies)
        {
            if (cuttingRecipeSo.input == inputKitchenObjectSo)
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
