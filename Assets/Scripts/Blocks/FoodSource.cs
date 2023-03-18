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
            if (player.CurrentItem.item == sourceItem)
            {
                player.RemoveItem();
            } else if (player.CurrentItem is KitchenItemEntity)
            {
                // Create Food Item and cast player current item to Kitchen Item Entity
                KitchenItemEntity playerKitchenItemEntity = (player.CurrentItem as KitchenItemEntity);
                FoodEntity newItem = sourceItem.Create();
                // If kitchen Item Entity can't hold source Item then we destroy it, else it placing on the Kitchen Item Entity
                if (!playerKitchenItemEntity.TryToAddItem(newItem))
                {
                    Destroy(newItem.gameObject);
                }
            }
            return;
        }
    }
}
