using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Block
{
    protected ItemEntity _itemEntity;
    public ItemEntity Item
    {
        get 
        { 
            return _itemEntity; 
        }
        set 
        { 
            _itemEntity = value;
            if (value == null) return;
            PlaceItem(_itemEntity);
        }
    }
    public override void Interact(PlayerController player)
    {
        if (_itemEntity == null && player.CurrentItem != null)
        {
            Item = player.CurrentItem;
            player.CurrentItem = null;
            return;
        }
        else if (_itemEntity != null) {
            // If player has an item we try to combine it
            if (player.CurrentItem != null)
            {
                // Check for players & table items if it's a FoodEntity then we make entity.item, Item to FoodItem and get output from CanCombine as combinedItem
                if (player.CurrentItem is FoodEntity && _itemEntity is FoodEntity)
                {
                    InteractFoodEntities(player.CurrentItem as FoodEntity);
                }
                // Check if player or a table has a KitchenItemEntity and table/player has a FoodItem try to put this item on the KitchenItemEntity
                else if ((player.CurrentItem is KitchenItemEntity && _itemEntity is FoodEntity)
                    || (player.CurrentItem is FoodEntity && _itemEntity is KitchenItemEntity)) // Try to add ingredient from table to Player Kitchen Entity Item
                {
                    InteractKitchenItemEntityWithFoodEntity(player);
                }
            }
            // Else just give player item if table has some of it
            else if (player.CurrentItem == null)
            {
                player.CurrentItem = _itemEntity;
                _itemEntity = null;
            }
        }
    }

    private void InteractFoodEntities(FoodEntity playerItem)
    {
        // Convert ItemEntity to FoodItem
        FoodEntity foodEntity = _itemEntity as FoodEntity;
        FoodEntity combinedFoodItem;

        // Check for a Combinations
        if (foodEntity.CanCombine(playerItem, out combinedFoodItem))
        {
            // Create new Item and put it on the Table
            Item = combinedFoodItem;
        }
    }

    private void InteractKitchenItemEntityWithFoodEntity(PlayerController player)
    {
        ItemEntity playerItem = player.CurrentItem;
        FoodEntity foodEntity;
        KitchenItemEntity kitchenItemEntity;
        // Set up base variables
        if (playerItem is KitchenItemEntity)
        {
            kitchenItemEntity = playerItem as KitchenItemEntity;
            foodEntity = _itemEntity as FoodEntity;
        }
        else
        {
            kitchenItemEntity = _itemEntity as KitchenItemEntity;
            foodEntity = playerItem as FoodEntity;
        }
        // We try to add item to Kitchen Item if that's right return
        if (kitchenItemEntity.TryToAddItem(foodEntity)) return;
        // Try to get cooked item from Kitchen Item and if it has it then continue
        FoodEntity cookedEntity = kitchenItemEntity.GetCookedItem();
        if (cookedEntity == null) return;
        // If Cooked item can combine with another item on table/player do this
        FoodEntity combinedFoodItem;
        if (!foodEntity.CanCombine(cookedEntity, out combinedFoodItem)) return;
        // Add this check because Kitchen item can be placed on the table then when items combining new item replace item on the table
        // so Kitchen Item exist only on the table and game doesn't see this item again
        if (playerItem is KitchenItemEntity)
        {
            Item = combinedFoodItem;
        } else
        {
            player.CurrentItem = combinedFoodItem;
        }
        // Clear cooked item on Kitchen Item
        kitchenItemEntity.RemoveItems();
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

