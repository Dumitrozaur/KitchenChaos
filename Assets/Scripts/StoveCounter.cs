using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private KitchenObject _kitchenObject;


    [SerializeField] private FryingRecipesSO[] fryingRecipesSoArray;

    private float fryingTimer;
    
    private void Update()
    {
        if (HasKitchenObject())
        {
            fryingTimer += Time.deltaTime;
            FryingRecipesSO fryingRecipesSo = GetFryingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
            if (fryingTimer > fryingRecipesSo.fryingTimerMax)
            {
             //Fried
             GetKitchenObject().DestroySelf();
             KitchenObject.SpawnKitchenObject(fryingRecipesSo.output, this);
            }
        }
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
