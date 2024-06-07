using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private KitchenObject _kitchenObject;
    
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

