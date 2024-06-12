using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplatePrefab;
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject[] ingredientList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += OnPlateIngridentAdded;
    }

    private void OnPlateIngridentAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        int index = 0;
        ingredientList = new GameObject[plateKitchenObject.GetIngredientList().Count];
        
        
        
        foreach (KitchenObjectSO kitchenObjectSo in plateKitchenObject.GetIngredientList())
        {
            for (int i = 0; i < ingredientList.Length; i++)
            {
                Destroy(ingredientList[i]);
            }
            
            iconImage.sprite = kitchenObjectSo.sprite;
            var instatiate = Instantiate(iconTemplatePrefab, transform);
            ingredientList[index] = instatiate.gameObject;
            index++;

        }
    }
}
