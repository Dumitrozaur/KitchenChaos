using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter, IKitchenObjectParent
{
    public override void Interact(Player player)
    {
        
        if(player.TryGetPlateObject(out PlateKitchenObject plateKitchenObject))
        {
            plateKitchenObject.DestroySelf();
        }
    }
}
