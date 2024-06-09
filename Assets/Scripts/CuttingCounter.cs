using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private KitchenObjectSO cutKitObjectSO;
    private KitchenObject _kitchenObject;

    [SerializeField] private CuttingRecipies[] cuttingRecipies;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchen object on the counter
            if (player.HasKitchenObject())
            {
                // Player is carrying a kitchen object
                KitchenObject playerKitchenObject = player.GetKitchenObject();
                if (IsKitchenObjectInRecipe(playerKitchenObject))
                {
                    // If the player's kitchen object is in the recipe list, place it on the counter
                    playerKitchenObject.SetKitchenObjectParent(this);
                }
            }
            else
            {
                // Player is not carrying anything
            }
        }
        else
        {
            // There is a kitchen object on the counter
            if (player.HasKitchenObject())
            {
                // Player is carrying a kitchen object
                KitchenObject playerKitchenObject = player.GetKitchenObject();
                if (IsKitchenObjectInRecipe(playerKitchenObject))
                {
                    // If the player's kitchen object is in the recipe list, place it on the counter
                    playerKitchenObject.SetKitchenObjectParent(this);
                }
            }
            else
            {
                // Player is not carrying anything, so they take the kitchen object from the counter
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            // There is a kitchen object here
            Debug.Log("CuttingCounter alternateInteract");
            KitchenObject kitchenObject = GetKitchenObject();
            KitchenObjectSO output = GetOutputForInput(kitchenObject.GetKitchenObjectSo());

            if (output != null)
            {
                kitchenObject.DestroySelf();
                KitchenObject newKitchenObject = Instantiate(output.prefab).GetComponent<KitchenObject>();
                newKitchenObject.SetKitchenObjectParent(this);
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var recipie in cuttingRecipies)
        {
            if (recipie.input == inputKitchenObjectSO)
            {
                return recipie.output;
            }
        }

        return null;
    }
    
    private bool IsKitchenObjectInRecipe(KitchenObject kitchenObject)
    {
        foreach (var recipe in cuttingRecipies)
        {
            if (recipe.input == kitchenObject.GetKitchenObjectSo())
            {
                return true;
            }
        }
        return false;
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
