using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;
    
    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (GetKitchenObject() == null)
            {
                Debug.Log("");
                Transform kitchenObjectTransform = Instantiate(_kitchenObjectSo.prefab, GetKitchenObjectFollowTransform());
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            }
            else
            {
                Debug.Log("Seteaza?");
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    
}
