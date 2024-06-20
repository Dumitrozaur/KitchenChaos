using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameCountdownTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownTimerText;

    private const int contdownToStart = 3;
    private void Start()
    {
        GameManager1.Instance.OnStateChange += InstanceOnOnStateChange;
    }

    private void InstanceOnOnStateChange(object sender, EventArgs e)
    {
        if (GameManager1.Instance.IsCountdownToStartActive())
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
        countDownTimerText.gameObject.SetActive(false);
        StopCoroutine(StartCounting());
    }

    private void Show()
    {
        countDownTimerText.gameObject.SetActive(true);
        StartCoroutine(StartCounting());
    }

    IEnumerator StartCounting()
    {
        float maxTime = contdownToStart;
        do
        {
            yield return new WaitForSeconds(1f);
            maxTime -= 1;
            countDownTimerText.text = maxTime.ToString();
            
        } while (maxTime > 0f);
        
    }
}
