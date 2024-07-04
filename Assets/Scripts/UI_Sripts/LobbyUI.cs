using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button quickJoinBtn;
    [SerializeField] private Button createLobbyBtn;
    [SerializeField] private Button joinCodeButton;
    [SerializeField] private LobbyCreateUI _lobbyCreateUI;
    [SerializeField] private TMP_InputField joinCode;
    [SerializeField] private Transform lobbyTemplate;
    [SerializeField] private Transform lobbyContainer;

    private void Start()
    {
        KitchenGameLobby.Instance.OnLobbyListChanged += InstanceOnOnLobbyListChanged;
        
        UpdateLobbyList(new List<Lobby>());
        
        lobbyTemplate.gameObject.SetActive(true);
    }

    private void InstanceOnOnLobbyListChanged(object sender, EventArgs e)
    {
        UpdateLobbyList(new List<Lobby>());
    }

    private void Awake()
    {
        quickJoinBtn.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.QuickJoin();
        });
        
        createLobbyBtn.onClick.AddListener(_lobbyCreateUI.Show);
        mainMenuBtn.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.LeaveLobby();
            LoadMainMenu();
        });
        
        joinCodeButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.JoinWithCode(joinCode.text);
        });
    }
    private void LoadMainMenu()
    {
        Loader.LoadNetwork(Loader.Scene.MenuScene);
    }

    private void UpdateLobbyList(List<Lobby> a)
    {
        foreach (Transform child in lobbyContainer)
        {
            if(child == lobbyTemplate) continue;
            
            Destroy(child);
        }
        /*
        foreach (Lobby lobby in lobbyList)
        {
            Transform newLobby = Instantiate(lobbyTemplate, lobbyContainer);
            newLobby.gameObject.SetActive(true);
        }*/
    }
}
