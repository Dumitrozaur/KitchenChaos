using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private KitchenObjectSO cutKitObjectSO;
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
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            //There is a obj here
            Debug.Log("CuttingCounter alternateInteract");
            GetKitchenObject().DestroySelf();
            Transform kitchenObjectTransform = Instantiate(cutKitObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
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
