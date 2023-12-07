using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingResipeSO cuttingResipeSO = GetCuttingResipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingResipeSO.cuttingProgressMax
                    });
                }
            }
            else
            {
                // Player not carrying anything
            }
        }
        else
        {
            //There is a KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingResipeSO cuttingResipeSO = GetCuttingResipeSOWithInput(inputKitchenObjectSO);
        return cuttingResipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingResipeSO cuttingResipeSO = GetCuttingResipeSOWithInput(inputKitchenObjectSO);
        if (cuttingResipeSO != null)
        {
            return cuttingResipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingResipeSO GetCuttingResipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingResipeSO cuttingResipeSO in cuttingRecipeSOArray)
        {
            if (cuttingResipeSO.input == inputKitchenObjectSO)
            {
                return cuttingResipeSO;
            }
        }
        return null;
    }
}
