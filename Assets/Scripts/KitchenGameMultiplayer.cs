using System;
using System.Collections.Generic;
using DefaultNamespace.SO_Scripts;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class KitchenGameMultiplayer : NetworkBehaviour
    {
        public static KitchenGameMultiplayer Instance { get; private set; }


        [SerializeField] private KitchenObjectListSO kitchenObjectListSO;

        private void Awake()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

        }
        
        public void StartHost()
        {
            NetworkManager.Singleton.ConnectionApprovalCallback += NetworkManager_ConnectionApprovalCallback;
            NetworkManager.Singleton.StartHost();
        }
        
        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }

        private void NetworkManager_ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest connectionApprovalRequest, NetworkManager.ConnectionApprovalResponse connectionApprovalResponse)
        {
            if (GameManager1.Instance.IsWaitingToSTart())
            {
                connectionApprovalResponse.Approved = true;
                connectionApprovalResponse.CreatePlayerObject = true;
            }
            else
            {
                connectionApprovalResponse.Approved = false;
            }
        
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
            
            NetworkObject kitchenObjectNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
            kitchenObjectNetworkObject.Spawn(true);
            
            KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            
            kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
            IKitchenObjectParent kitchenObjectParent =
                kitchenObjectParentNetworkObject.GetComponent<IKitchenObjectParent>();

            kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
            if (kitchenObjectParent.HasKitchenObject())
            {
                // Parent already spawned an object
                return;
            }
            
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
