using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsBindingController : MonoBehaviour
{
    [SerializeField] private Button moveUpButton;
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
    [SerializeField] private Transform pressToRebindKeyTransform;

    private void Awake()
    {
        moveUpButton.onClick.AddListener((() => {RebindBinding(GameInput.Bindings.MoveUp);}));
        moveDown.onClick.AddListener((() => {RebindBinding(GameInput.Bindings.MoveDown);}));
        moveLeft.onClick.AddListener((() => {RebindBinding(GameInput.Bindings.MoveLeft);}));
        moveRight.onClick.AddListener((() => {RebindBinding(GameInput.Bindings.MoveRight);}));
        interact.onClick.AddListener((() => {RebindBinding(GameInput.Bindings.Interact);}));
        alternateInteract.onClick.AddListener((() => {RebindBinding(GameInput.Bindings.AlternateInteract);}));
        pause.onClick.AddListener((() => {RebindBinding(GameInput.Bindings.Pause);}));

    }

    private void OnEnable()
    {
        UpdateVisula();
        HidePressToRebindKey();

    }
    private void UpdateVisula()
    {
        moveUpText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveUp);
        moveDownText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveDown);
        moveLeftText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveLeft);
        moveRightText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.MoveRight);
        alternateInteractText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.AlternateInteract);
        interactText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.Interact);
        pauseText.text = GameInput.Instance.GetBndingText(GameInput.Bindings.Pause);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Bindings bindings)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(bindings, () =>
        {
            HidePressToRebindKey();
            UpdateVisula();
        });
    }
    
    
}
