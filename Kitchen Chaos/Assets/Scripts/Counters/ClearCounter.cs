using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
   public override void Interact(Player player)
    {
            if(!HasKitchenObject())
        {
            // there is no kitchenObbject here
            if (player.HasKitchenObject())
            { //player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            { //player not carrying anything

            }
        }
        else
        {
            // there is a kitchenobject here
            if(!player.HasKitchenObject()) 
            { //player not carrying anything eu inverti a ordem do monkey, se der problema verificar aqui
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else 
            { // player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                { //player is holding a plate                    
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) 
                    {
                        GetKitchenObject().DestroySelf(); 
                    }
                    
                }
                else
                {// player is not carrying plate but something else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    { //Counter is Holding a plate
                      if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
    }

   
}
