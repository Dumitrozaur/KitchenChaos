using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipiesDeliveredText;
    
    private void Start()
    {
        GameManager1.Instance.OnStateChange += InstanceOnOnStateChange;
        
        Hide();
    }

    private void InstanceOnOnStateChange(object sender, EventArgs e)
    {
        if (GameManager1.Instance.IsGameOver())
        {
            Show();
        }
        else
        {
            Hide();
        }
        
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void Show()
    {
        this.gameObject.SetActive(true);
        recipiesDeliveredText.text = DeliveryManager.Instance.GetSuccessfullDeliveries().ToString();
    }
    
}
