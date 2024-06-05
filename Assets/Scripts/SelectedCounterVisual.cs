using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private ClearCounter _clearCounter;
    [SerializeField] private GameObject selectedCounter;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
    }

    private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (_clearCounter == e.selectedCounter)
        {
            Show();    
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        selectedCounter.SetActive(true);
    }

    private void Hide()
    {
        selectedCounter.SetActive(false);
    }
}

