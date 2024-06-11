using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter, IKitchenObjectParent
{
    //IKit.
    [SerializeField] private Transform kitchenObjectHoldPoint; 
    private KitchenObject _kitchenObject;
    //IKit.

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    
    
    [SerializeField] private KitchenObjectSO plateObject;
    private float maxTimeSpawn = 4f;
    private float timeBetweenSpawns = 4f;
    private int platesAmount;
    private int maxPlatesAmount = 4;

    private void Start()
    {
        platesAmount = 4;
        for (int plates = 0; plates < platesAmount; plates++) {
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        timeBetweenSpawns += Time.deltaTime;
        if (timeBetweenSpawns > maxTimeSpawn)
        {
            timeBetweenSpawns = 0;
            if (platesAmount < maxPlatesAmount)
            {
                platesAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesAmount > 0)
            {
                platesAmount--;

                KitchenObject.SpawnKitchenObject(plateObject, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    //IKit.
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
    //IKit.
}
