using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSource : Block
{
    /// <summary>
    /// Item which is will be spawned each time when player interact with it
    /// </summary>
    [SerializeField]
    private FoodItem _sourceItem;

    public override void Interact(ChefController player)
    {
        // Check if player has an ItemEntity
        if (player.CurrentItem == null)
        {
            player.CurrentItem = _sourceItem.Create();
        } else if (player.CurrentItem != null)
        {
            if (player.CurrentItem.item == _sourceItem)
            {
                // If Player Item is Food Entity we check for additional Items on it, and if it has something we dont remove player item
                if (player.CurrentItem is FoodEntity && (player.CurrentItem as FoodEntity).addedItems.Count > 0)
                {
                    return;
                }
                player.RemoveItem();
            } else if (player.CurrentItem is KitchenItemEntity)
            {
                // Create Food Item and cast player current item to Kitchen Item Entity
                KitchenItemEntity playerKitchenItemEntity = (player.CurrentItem as KitchenItemEntity);
                FoodEntity newItem = _sourceItem.Create();
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
