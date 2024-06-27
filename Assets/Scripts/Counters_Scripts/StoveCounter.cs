using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
   public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

   public class OnStateChangedEventArgs : EventArgs
   {
      public State state;
   }
   
   public enum State
   {
      Idle,
      Frying,
      Fried,
      Burned,
   }
   [SerializeField] private FryingRecipesSO[] _fryingReceipeSosArray;
   [SerializeField] private BurningRecipeSO[] _burningReceipeSosArray;

   private float fryingTimer;
   private float burningTimer;
   private BurningRecipeSO burningReceipeSo;
   private FryingRecipesSO fryingReceipeSo;

   private State state;

   private void Start()
   {
      state = State.Idle;
   }

   private void Update()
   {
      if (HasKitchenObject())
      {

         switch (state)
         {
            case State.Idle:
               break;
            case State.Frying:
               fryingTimer += Time.deltaTime;
               
               OnProgressChanged?.Invoke(this,new IHasProgress .OnProgressChangedEventArgs
               {
                  ProgressNormalized = fryingTimer / fryingReceipeSo.fryingTimerMax
               });
               
               if (fryingTimer > fryingReceipeSo.fryingTimerMax)
               {
                  
                  GetKitchenObject().DestroySelf();

                  KitchenObject.SpawnKitchenObject(fryingReceipeSo.output, this);
                
                  state = State.Fried;
                  burningTimer = 0f;
                  burningReceipeSo = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSo());
                  OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                     state = state
                     });
               }
               break;
            case State.Fried:
               burningTimer += Time.deltaTime;
               OnProgressChanged?.Invoke(this,new IHasProgress .OnProgressChangedEventArgs
               {
                  ProgressNormalized = burningTimer / burningReceipeSo.burningTimerMax
               });
               if (burningTimer > burningReceipeSo.burningTimerMax)
               {
                  
                  GetKitchenObject().DestroySelf();

                  KitchenObject.SpawnKitchenObject(burningReceipeSo.output, this);
                  
                  state = State.Burned;
                  OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                     state = state
                     
                  });
                  
                  OnProgressChanged?.Invoke(this,new IHasProgress .OnProgressChangedEventArgs
                  {
                     ProgressNormalized = 0f
                  });
               }
               break;
            case State.Burned:
               break;
         }
        
      }


   }

   public override void Interact(Player player)
   {
      if (!HasKitchenObject())
      {
         if (player.HasKitchenObject())
         {
            if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo()))
            {
               player.GetKitchenObject().SetKitchenObjectParent(this);
               fryingReceipeSo = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSo());
               state = State.Frying;
               fryingTimer = 0f;
               OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                  state = state
               });
               OnProgressChanged?.Invoke(this,new IHasProgress .OnProgressChangedEventArgs
               {
                  ProgressNormalized = fryingTimer / fryingReceipeSo.fryingTimerMax
               });

            }
         }


      }
      else
      {
         if (player.HasKitchenObject())
         {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                 
               if ( plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSo()))
               {
                  GetKitchenObject().DestroySelf();
                  state = State.Idle;
                  OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                     state = state
                  });
                  OnProgressChanged?.Invoke(this,new IHasProgress .OnProgressChangedEventArgs
                  {
                     ProgressNormalized = 0f
                  });
                      
               }
            }
         }
         else
         {
            GetKitchenObject().SetKitchenObjectParent(player);
            state = State.Idle;
            OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{ 
               state = state
            });
            OnProgressChanged?.Invoke(this,new IHasProgress .OnProgressChangedEventArgs {
               ProgressNormalized = 0f
            });
         }
      }
   }
   private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
   {
      FryingRecipesSO fryingReceipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
      return fryingReceipeSo != null;
   }

   private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
   {
      FryingRecipesSO fryingReceipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
      if (fryingReceipeSo != null)
         return fryingReceipeSo.output;
      else
      {
         return null;
      }
      
   }

   private FryingRecipesSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
   {
      foreach (FryingRecipesSO fryingReceipeSo in _fryingReceipeSosArray)
      {
         if (fryingReceipeSo.input == inputKitchenObjectSO)
         {
            return fryingReceipeSo;
         }
      }

      return null;
   }
   private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
   {
      foreach (BurningRecipeSO burningReceipeSo in _burningReceipeSosArray)
      {
         if (burningReceipeSo.input == inputKitchenObjectSO)
         {
            return burningReceipeSo;
         }
      }

      return null;
   }
}
