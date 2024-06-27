using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlateCounter : BaseCounter
{

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
        if (!IsServer) return;
        timeBetweenSpawns += Time.deltaTime;
        if (timeBetweenSpawns > maxTimeSpawn)
        {
            timeBetweenSpawns = 0;
            if (GameManager1.Instance.IsGamePlaying() && platesAmount < maxPlatesAmount)
            {
                SpawnPlateServerRpc();
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesAmount > 0)
            {
                

                KitchenObject.SpawnKitchenObject(plateObject, player);
                InteractLogicServerRpc();
            }
        }
    }

    [ServerRpc]
    private void SpawnPlateServerRpc()
    {
        SpawnPlateClientRpc();
    }

    [ClientRpc]
    private void SpawnPlateClientRpc()
    {
        platesAmount++;
        OnPlateSpawned?.Invoke(this, EventArgs.Empty);
    }
    
    [ServerRpc]
    private void InteractLogicServerRpc()
    {
        InteractLogicClientRpc();
    }

    [ClientRpc]
    private void InteractLogicClientRpc()
    {
        platesAmount--;
        OnPlateSpawned?.Invoke(this, EventArgs.Empty);
    }
    
}
