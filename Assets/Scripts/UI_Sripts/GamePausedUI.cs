using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private OtherOptionsUI volumeControlerPanel;
    [SerializeField] private VolumeControler volumeControler;

    
    private void Awake()
    {
        resumeButton.onClick.AddListener((() =>
        {
            GameManager1.Instance.TogglePauseGame();
        }));
        
        optionsButton.onClick.AddListener(OnOptionsMenuButton);
        
        mainMenuButton.onClick.AddListener((() =>
        {
            Loader.Load(Loader.Scene.MenuScene);
        }));
        volumeControlerPanel.Show();
        Time.timeScale = 1f;
    }

    private void OnOptionsMenuButton()
    {
        volumeControler.UpdateSliders();
    }

    private void Start()
    {
        GameManager1.Instance.OnGamePaused += Instance_OnGamePaused;
        GameManager1.Instance.OnGameUnpaused += Instance_OnGameUnpaused;
        Hide();
    }
    

    private void Instance_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void Instance_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
