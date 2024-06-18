using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsBindingController : MonoBehaviour
{
    [SerializeField] private Button moveUp;
    [SerializeField] private Button moveDown;
    [SerializeField] private Button moveLeft;
    [SerializeField] private Button moveRight;
    [SerializeField] private Button interact;
    [SerializeField] private Button alternateInteract;
    [SerializeField] private Button pause;
    
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI alternateInteractText;
    [SerializeField] private TextMeshProUGUI pauseText;

    private void OnEnable()
    {
        UpdateVisula();
    }

    private void UpdateVisula()
    {
        moveUpText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveUp);
        moveDownText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveDown);
        moveLeftText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveLeft);
        moveRightText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveRight);

    }
    
    
}
