using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherOptionsUI : MonoBehaviour
{

    [SerializeField] private Button showTutorialButton;
    [SerializeField] private TutorialUI _tutoriaUi;

    private void Start()
    {


        showTutorialButton.onClick.AddListener(showTutorialWindow);
        Hide();

    }

    private void showTutorialWindow()
    {
        _tutoriaUi.Show();
    }



    private void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {



        gameObject.SetActive(true);
    }







}