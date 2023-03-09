using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSource : Block
{
    public FoodItem sourceItem;

    public override void Interact(PlayerController player)
    {
        // Check if player has an ItemEntity
        if (player.CurrentItem == null)
        {
            player.CurrentItem = sourceItem.Create();
        } else if (player.CurrentItem != null)
        {
            print(player.CurrentItem + " : " + player.CurrentItem.item + " : " + sourceItem);
            if (player.CurrentItem.item == sourceItem)
            {
                player.RemoveItem();
            }
            return;
        }
    }
}
