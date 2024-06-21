using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : NetworkBehaviour
{
    public event EventHandler OnRecipeAdded;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeList _recipeList;
    private List<RecipeSo> waitingRecipeSOList=new List<RecipeSo>();


    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfullDelivered;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(!IsServer)
        {
            return;
        }
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax; 
            
            if (GameManager1.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax)
            {
                int waitingRecipeSoIndex = Random.Range(0, _recipeList.recipeSOList.Count);
                RecipeSo waitingRecipeSo = _recipeList.recipeSOList[waitingRecipeSoIndex];
                SpawnedNewWaitingRecipeClientRpc(waitingRecipeSoIndex);

            }
        }
        
    }
    [ClientRpc]
    private void SpawnedNewWaitingRecipeClientRpc(int waitingRecipeSoIndex)
    {
        RecipeSo waitingRecipeSo = _recipeList.recipeSOList[waitingRecipeSoIndex];
        waitingRecipeSOList.Add(waitingRecipeSo);
        OnRecipeAdded?.Invoke(this, EventArgs.Empty);
    }
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSo waitingRecipeSo = waitingRecipeSOList[i];

            if (waitingRecipeSo.KitchenObjectSoList.Count == plateKitchenObject.GetIngredientList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                //Has the same number of ingridients
                foreach (KitchenObjectSO recipeKitchenObjectSo in waitingRecipeSo.KitchenObjectSoList)
                {
                    bool ingridientFound = false;
                    //Cycling through all ingridients in the recipe
                    foreach (KitchenObjectSO plateKitchenObjectSo in plateKitchenObject.GetIngredientList())
                    {
                        //Through all ingridients in the plate
                        Debug.Log(plateKitchenObject.GetIngredientList());
                        if (plateKitchenObjectSo == recipeKitchenObjectSo)
                        {
                            //ingridient match
                            ingridientFound = true;
                            break;
                        }
                    }

                    if (!ingridientFound)
                    {
                        //ingridient was not found on the plate
                        Debug.Log("Recipe was not found!");
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    //Player deliver the correct recipe
                    successfullDelivered++;
                    
                    Debug.Log("Player deliver the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        //No matches
        //Incorect recipe
    }

    public List<RecipeSo> GetWaitingRecipesSO()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfullDeliveries()
    {
        return successfullDelivered;
    }
}