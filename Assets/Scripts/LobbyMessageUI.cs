using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class LobbyMessageUI : MonoBehaviour
{
    private void Start()
    {
        KitchenGameLobby.Instance.OnFailedLobbyStarted += KitchenGameMultiplayer_OnFailedToJoin;
        KitchenGameLobby.Instance.OnCreateLobbyStarted += KitchenGameMultiplayer_OnCreateToJoin;
        KitchenGameLobby.Instance.OnDestroyLobbyStarted += KitchenGameMultiplayer_OnDestroyLobby;
        KitchenGameLobby.Instance.OnLeaveLobbyStarted += KitchenGameMultiplayer_OnLeaveLobby;
        KitchenGameLobby.Instance.OnQuickJoinedFailed+= KitchenGameMultiplayer_OnQuickJoinFailed;
        KitchenGameLobby.Instance.OnQuickJoinedSuccess+= KitchenGameMultiplayer_OnQuickJoinSuccsess;

    }

    private void KitchenGameMultiplayer_OnQuickJoinSuccsess(object sender, EventArgs e)
    {
        ShowMessage("Quick Join to the Lobby Succeeded");
    }

    private void KitchenGameMultiplayer_OnQuickJoinFailed(object sender, EventArgs e)
    {
        ShowMessage("Quick Join to the Lobby failed...");
    }

    private void KitchenGameMultiplayer_OnLeaveLobby(object sender, EventArgs e)
    {
        ShowMessage("You leave the lobby!");
    }

    private void KitchenGameMultiplayer_OnDestroyLobby(object sender, EventArgs e)
    {
        ShowMessage("Lobby was destroyed..");
    }

    private void KitchenGameMultiplayer_OnCreateToJoin(object sender, EventArgs e)
    {
        ShowMessage("Failed to create Lobby...");
    }


    [SerializeField] private TextMeshProUGUI messageText;
    private void ShowMessage(string message)
    {
        messageText.text = message;
        
    }


    private void KitchenGameMultiplayer_OnFailedToJoin(object sender, EventArgs e)
    {
        if (NetworkManager.Singleton.DisconnectReason != "")
        {
            ShowMessage(NetworkManager.Singleton.DisconnectReason);
        }
        else
        {
            //Show();
            messageText.text = NetworkManager.Singleton.DisconnectReason;
        }
    }
}
