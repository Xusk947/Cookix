using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Block
{
    public override void Interact(ChefController player)
    {
        if (player.CurrentItem == null) return;
        switch(player.CurrentItem)
        {
            case FoodEntity:
                player.RemoveItem();
                break;
            case KitchenItemEntity:
                (player.CurrentItem as KitchenItemEntity).RemoveItems();
                break;
        }
    }
}
