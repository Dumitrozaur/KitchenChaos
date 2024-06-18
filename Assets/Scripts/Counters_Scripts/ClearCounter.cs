using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no kitObj
            if (player.HasKitchenObject())
            {
                //Player carrying
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Not caring
            }
        }
        else
        {
            //ther is 
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSo()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //Player not carry plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSo()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //Not carring
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            {
                
            }
        }
    }
}

