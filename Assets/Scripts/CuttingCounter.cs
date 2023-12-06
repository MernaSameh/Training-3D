using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingResipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //There is a KitchenObject here AND it can be cut
            cuttingProgress++;

            CuttingResipeSO cuttingResipeSO = GetCuttingResipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            if (cuttingProgress >= cuttingResipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
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
