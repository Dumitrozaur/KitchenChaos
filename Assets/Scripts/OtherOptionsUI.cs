using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OtherOptionsUI : MonoBehaviour
{
    [SerializeField] private Button closeOptionButton;
    [SerializeField] private Button showTutorialButton;
    [SerializeField] private TutorialUI tutorialUI;
    [SerializeField] private VolumeControler _volumeControlerUI;
    private Action onCloseButtonAction;

    
    private void Start()
    {
        closeOptionButton.onClick.AddListener((() =>
        {
            CloseOptions();
            onCloseButtonAction();
        }));   
        showTutorialButton.onClick.AddListener(ShowTutorialWindow);
        
    }

    private void ShowTutorialWindow()
    {
        tutorialUI.Show();
    }

    private void CloseOptions()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        this.onCloseButtonAction = onCloseButtonAction;
        _volumeControlerUI.UpdateSliders();
        gameObject.SetActive(true);
        closeOptionButton.Select();
    }
}
