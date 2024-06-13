using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
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

                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSo()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
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

            
            
            OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
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
    
}
