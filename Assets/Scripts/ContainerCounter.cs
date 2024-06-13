using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;
    [SerializeField] private Transform counterTopPoit;
    
    private KitchenObject _kitchenObject;
    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (_kitchenObject == null)
            {
                Transform kitchenObjectTransform = Instantiate(_kitchenObjectSo.prefab, counterTopPoit);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            }
            else
            {
                _kitchenObject.SetKitchenObjectParent(player);
            }
        }
    }

    
}
