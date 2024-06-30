using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForOtherPlayersUI : MonoBehaviour
{
    private void Start()
    {
        Hide();
        GameManager1.Instance.OnStatePlayerReady += OnPlayerReady;
    }

    private void OnPlayerReady(object sender, EventArgs e)
    {
        if (GameManager1.Instance.isLocalPlayerReady = true)
        {
            Debug.Log("Show Waiting");
            Show();
        }
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
