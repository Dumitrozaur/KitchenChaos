using System;
using System.Collections.Generic;
using DefaultNamespace.SO_Scripts;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class KitchenGameMultiplayer : NetworkBehaviour
    {
        public static KitchenGameMultiplayer Instance { get; private set; }


        [SerializeField] private KitchenObjectListSO kitchenObjectListSO;

        private void Awake()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

        }

        private void Start()
        {

        }
        
        public void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
        {
            SpawnKitchenObjectServerRpc(GetKitchenObjectSOIndex(kitchenObjectSO), kitchenObjectParent.GetNetworkObject());
        }

        [ServerRpc(RequireOwnership = false)]
        private void SpawnKitchenObjectServerRpc(int kitchenObjectSOIndex,NetworkObjectReference kitchenObjectParentNetworkObjectReference)
        {
            KitchenObjectSO kitchenObjectSO = GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            
            kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
            IKitchenObjectParent kitchenObjectParent =
                kitchenObjectParentNetworkObject.GetComponent<IKitchenObjectParent>();

            
            if (kitchenObjectParent.HasKitchenObject())
            {
                // Parent already spawned an object
                return;
            }

            

            NetworkObject kitchenObjectNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
            kitchenObjectNetworkObject.Spawn(true);

            KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();


            kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        }

        public int GetKitchenObjectSOIndex(KitchenObjectSO kitchenObjectSO)
        {
            return kitchenObjectListSO.kitchenObjectSOList.IndexOf(kitchenObjectSO);
        }

        public KitchenObjectSO GetKitchenObjectSOFromIndex(int kitchenObjectSOIndex)
        {
            return kitchenObjectListSO.kitchenObjectSOList[kitchenObjectSOIndex];
        }

        public void DestroyKitchenObject(KitchenObject kitchenObject)
        {
            DestroyKitchenObjectServerRpc(kitchenObject.NetworkObject);
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void DestroyKitchenObjectServerRpc(NetworkObjectReference networkObjectReference)
        {
            networkObjectReference.TryGet(out NetworkObject networkObject);
            KitchenObject kitchenObject = networkObject.GetComponent<KitchenObject>();
            ClearKitchenParentClientRpc(networkObjectReference);
            kitchenObject.DestroySelf();
        }

        [ClientRpc]
        public void ClearKitchenParentClientRpc(NetworkObjectReference networkObjectReference)
        {
            networkObjectReference.TryGet(out NetworkObject networkObject);
            KitchenObject kitchenObject = networkObject.GetComponent<KitchenObject>();
            
            kitchenObject.ClearKitchenObjectOnParent();
        }
    
       
    }
}