using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyAlternateInteractText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    
    [SerializeField] private Transform tutorialCanvas;
    
    private const string PLAYER_PREFS_TUTORIAL_AUTOSHOW = "TutorialAutoShow";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PLAYER_PREFS_TUTORIAL_AUTOSHOW) == false)
        {
            Show();
            PlayerPrefs.SetInt(PLAYER_PREFS_TUTORIAL_AUTOSHOW,0);
        }
        else
        {
            Hide(this, EventArgs.Empty);
        }
    }

    private void Start()
    {
        GameInput.Instance.OnInteractAction += Hide;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveUp);
        keyMoveDownText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveDown);
        keyMoveLeftText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveLeft);
        keyMoveRightText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveRight);
        keyInteractText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.Interact);
        keyAlternateInteractText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.AlternateInteract);
        keyPauseText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.Pause);

        
    }
    
    private void Hide(object sender, EventArgs e)
    {
        tutorialCanvas.gameObject.SetActive(false);
    }
    public void Show()
    {
        tutorialCanvas.gameObject.SetActive(true);
    }
}
