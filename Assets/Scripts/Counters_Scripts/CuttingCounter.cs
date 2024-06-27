using System;
using Unity.Netcode;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{

    public static event EventHandler OnAnyCut;

    public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
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
                    KitchenObject kitchenObject = player.GetKitchenObject();
                    kitchenObject.SetKitchenObjectParent(this);

                    InteractLogicPlaceObjectOnCounterServerRpc();
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
            CutObjectServerRpc();
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void InteractLogicPlaceObjectOnCounterServerRpc()
    {
        InteractLogicPlaceObjectOnCounterClientRpc();
    }
    [ClientRpc]
    private void InteractLogicPlaceObjectOnCounterClientRpc()
    {
        
        cuttingProgress = 0;

        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
        {
            ProgressNormalized =  0f
        });
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void CutObjectServerRpc()
    {
        CutObjectClientRpc();
        TestCuttingProgressDoneServerRpc();
    }
    [ClientRpc]
    private void CutObjectClientRpc()
    {
        cuttingProgress++;
        
        CuttingRecipies cuttingRecipeSo = GetcuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
        
        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
        {
            ProgressNormalized =  (float)cuttingProgress / cuttingRecipeSo.cuttingMAX
        });
        
        
    }
    [ServerRpc(RequireOwnership = false)]
    private void TestCuttingProgressDoneServerRpc()
    {
        CuttingRecipies cuttingRecipeSo = GetcuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
        if (cuttingProgress >= cuttingRecipeSo.cuttingMAX)
        {
            
            KitchenObjectSO outputKitchenObjcetSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSo());
            KitchenObject.DestroyKitchenObject(GetKitchenObject());
            KitchenObject.SpawnKitchenObject(outputKitchenObjcetSo, this);
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
