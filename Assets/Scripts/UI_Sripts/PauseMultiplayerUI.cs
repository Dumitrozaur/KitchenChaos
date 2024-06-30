using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMultiplayerUI : MonoBehaviour
{
    private void Start()
    {
        GameManager1.Instance.OnMultiplayerGamePaused += KitchenGameManager_OnMultiplayerGamePaused;
        GameManager1.Instance.OnMultiplayerGameUnpaused += KitchenGameManager_OnMultiplayerGameUnpaused;

        Hide();
    }

    private void KitchenGameManager_OnMultiplayerGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnMultiplayerGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}

