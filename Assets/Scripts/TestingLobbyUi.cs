using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class TestingLobbyUi : MonoBehaviour
{
    [SerializeField] private Button createGameButton;
    [SerializeField] private Button joinGameButton;

    private void Awake()
    {
        createGameButton.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.Instance.StartHost();
            Loader.LoadNetwork(Loader.Scene.CharacterSelectScene);
            
        });
        joinGameButton.onClick.AddListener( () =>
        {
            Debug.Log("intra?");
            KitchenGameMultiplayer.Instance.StartClient();
            Loader.LoadNetwork(Loader.Scene.CharacterSelectScene);
        });

    }
}
