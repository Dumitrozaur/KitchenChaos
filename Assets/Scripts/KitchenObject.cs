using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO _kitchenObjectSo;

   private ClearCounter _clearCounter;
   
   public KitchenObjectSO GetKitchenObjectSo()
   {
      return _kitchenObjectSo;
   }

   public void SetClearCounter(ClearCounter clearCounter)
   {
       if(this._clearCounter != null){
           this._clearCounter.ClearKitchenObject();
           
       }
       
       this._clearCounter = clearCounter;
       clearCounter.SetKitchenObject(this);
       
       transform.parent = clearCounter.GetKitchenObjectFollowTransform();
       transform.localPosition = UnityEngine.Vector3.zero;
   }

   public ClearCounter GetClearCounter()
   {
       return _clearCounter;
   }
}
