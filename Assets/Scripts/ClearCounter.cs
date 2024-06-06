using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;
    [SerializeField] private Transform counterTopPoit;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;
    
    private KitchenObject _kitchenObject;


    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (_kitchenObject != null)
            {
                _kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }

    public void Iteract()
    {
        if (_kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(_kitchenObjectSo.prefab, counterTopPoit);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(_kitchenObject.GetClearCounter());
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoit;
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

