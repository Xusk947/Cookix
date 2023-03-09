using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Block
{
    protected ItemEntity itemEntity;
    public ItemEntity Item
    {
        get 
        { 
            return itemEntity; 
        }
        set 
        { 
            itemEntity = value;
            if (value == null) return;
            PlaceItem(itemEntity);
        }
    }
    public override void Interact(PlayerController player)
    {
        if (itemEntity == null && player.CurrentItem != null)
        {
            Item = player.CurrentItem;
            player.CurrentItem = null;
            return;
        }
        else if (itemEntity != null) {
            // If player has an item we try to combine it
            if (player.CurrentItem != null)
            {
                // Check for players & table items if it's a FoodEntity then we make entity.item, Item to FoodItem and get output from CanCombine as combinedItem
                if (player.CurrentItem is not FoodEntity) return;
                if (itemEntity is not FoodEntity) return;
                // Convert ItemEntity to FoodItem
                FoodEntity foodEntity = itemEntity as FoodEntity;
                FoodEntity playerItem = player.CurrentItem as FoodEntity;
                FoodEntity combinedFoodItem;

                // Check for a Combinations
                if (foodEntity.CanCombine(playerItem, out combinedFoodItem))
                {
                    // Create new Item and put it on the Table
                    Item = combinedFoodItem;
                }
            }
            else if (player.CurrentItem == null)
            {
                player.CurrentItem = itemEntity;
                itemEntity = null;
            }
        }
    }

    protected virtual void PlaceItem(ItemEntity item)
    {
        RaycastHit hit;
        // To set item directly to top of block, we use raycast, 5 value used to set raycast point higher than table
        if (Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down, out hit, Mathf.Infinity))
        {
            item.transform.parent = transform;
            item.transform.position = hit.point;
        }
    }
}

