using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : BaseCounter
{

    public static event EventHandler OnAnyObjectTrahsed;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            
            OnAnyObjectTrahsed?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
