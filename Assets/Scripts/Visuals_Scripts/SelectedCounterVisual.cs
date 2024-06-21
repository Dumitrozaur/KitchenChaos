using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter currentCounter;
    [SerializeField] private GameObject[] selectedCounter;

    private void Start()
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSelectedCounterChanged -= OnSelectedCounterChanged;
            Player.LocalInstance.OnSelectedCounterChanged += OnSelectedCounterChanged;
        }else
        {
            Player.OnAnyPlayerSpawned += PlayerSpawned;
        }
   
    }

    private void PlayerSpawned(object sender, EventArgs e)
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSelectedCounterChanged -= OnSelectedCounterChanged;
            Player.LocalInstance.OnSelectedCounterChanged += OnSelectedCounterChanged;
        }
    }

    private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        foreach (var selectedVisual in selectedCounter)
        {
            selectedVisual.SetActive(e.selectedCounter == currentCounter);
        }
    }


}

