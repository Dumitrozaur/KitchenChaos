using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GamePausedUI : MonoBehaviour
{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private VolumeController volumeControllerPanel;
    [SerializeField] private Button optionsMenuButton;
   // public string mainMenuSceneName = "Assets/Scenes/StartScene"; 
    public string gameSceneName = "GameScene";
#if UNITY_EDITOR
    [SerializeField] private SceneAsset mainMenuSceneAsset;
#endif
    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        optionsMenuButton.onClick.AddListener(OnOptionMenuButton);
        
        MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

}

    private void OnOptionMenuButton()
    {
        volumeControllerPanel.Show();
        
    }

    private void OnResumeButtonClicked()
    {
        GameManager1.Instance.TogglePauseGame();
    }

    private void OnMainMenuButtonClicked()
    {
#if UNITY_EDITOR
        if (mainMenuSceneAsset != null)
        {
            string scenePath = AssetDatabase.GetAssetPath(mainMenuSceneAsset);
            SceneManager.LoadScene(scenePath);
        }
#else
        Debug.Log("Main Menu button clicked. Loading Main Menu scene.");
        SceneManager.LoadScene("StartScene"); 
#endif
    }

    private void Start()
    {
        GameManager1.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        GameManager1.Instance.OnGameUnpaused += KitchenGameManage_OnGameUNPaused;
        Hide();
    }

    private void KitchenGameManage_OnGameUNPaused(object sender, EventArgs e)
    {
       Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, EventArgs e)
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