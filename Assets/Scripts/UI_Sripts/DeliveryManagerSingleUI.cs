using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipieSO(RecipeSo recipeSo)
    {
        recipeNameText.text = recipeSo.name;

        foreach (Transform child in iconContainer)
        {
            if (child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var kitchenObjectSo in recipeSo.KitchenObjectSoList)
        {
            var instantiateIcon = Instantiate(iconTemplate, iconContainer);
            instantiateIcon.gameObject.SetActive(true);
            var image = instantiateIcon.transform.GetComponent<Image>();
            image.sprite = kitchenObjectSo.sprite;
        }
    }
    
}
