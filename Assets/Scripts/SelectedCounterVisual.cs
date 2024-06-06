using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter currentCounter;
    [SerializeField] private GameObject[] selectedCounter;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
    }
    
    private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        foreach (var selectedVisual in selectedCounter)
        {
            selectedVisual.SetActive(e.selectedCounter == currentCounter);
        }
    }


}

