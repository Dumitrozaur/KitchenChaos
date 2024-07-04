using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCreateUI : MonoBehaviour
{
    public LobbyCreateUI Instance { get;private set;}
    
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button createPrivateLobbyBtn;
    [SerializeField] private Button createPublicLobbyBtn;
    [SerializeField] public TMP_InputField lobbyNameInputField;

    private void Start()
    {
        Hide();
    }

    private void Awake()
    {
        Instance = this;
        
        createPublicLobbyBtn.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text, false);
        });
        createPrivateLobbyBtn.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text, true);
        });
        closeBtn.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
}
