using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO _kitchenObjectSo;

   private IKitchenObjectParent kitchenObjectPare;
   
   public KitchenObjectSO GetKitchenObjectSo()
   {
      return _kitchenObjectSo;
   }

   public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
   {
       if(this.kitchenObjectPare != null){
           this.kitchenObjectPare.ClearKitchenObject();
           
       }
       
       this.kitchenObjectPare = kitchenObjectParent;
       kitchenObjectPare.SetKitchenObject(this);
       
       transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
       transform.localPosition = UnityEngine.Vector3.zero;
   }

   public IKitchenObjectParent GetKitchenObject()
   {
       return kitchenObjectPare;
   }

   public void DestroySelf()
   {
       kitchenObjectPare.ClearKitchenObject();

       Destroy(gameObject);
   }
   
   public static KitchenObject SpawnKitchenObject(KitchenObjectSO newKitchenObjectSO, IKitchenObjectParent newIKichenObjectParent)
   {
       Transform KitchenObjectTransform = Instantiate(newKitchenObjectSO.prefab);
       KitchenObject kitchenObject = KitchenObjectTransform.GetComponent<KitchenObject>();
       kitchenObject.SetKitchenObjectParent(newIKichenObjectParent);
       return kitchenObject;
   }
}
