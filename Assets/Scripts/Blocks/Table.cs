using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Block
{
    /// <summary>
    /// Item Entity on the table
    /// </summary>
    protected ItemEntity _itemEntity;
    /// <summary>
    /// Item on the table, when set a new variable it's automaticlly place on it
    /// </summary>
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
    public override void Interact(ChefController player)
    {
        if (_itemEntity == null && player.CurrentItem != null)
        {
            TakeItemFromPlayer(player);
            return;
        }
        else if (_itemEntity != null) 
        {
            switch(player.CurrentItem)
            {
                case null:
                    GivePlayerItemFromTable(player);
                    break;
                case FoodEntity:
                    if (_itemEntity is FoodEntity) InteractFoodEntities(player, player.CurrentItem as FoodEntity);
                    else if (_itemEntity is KitchenItemEntity) InteractKitchenItemEntityWithFoodEntity(player, _itemEntity as KitchenItemEntity, player.CurrentItem as FoodEntity);
                    else if (_itemEntity is PlateEntity) InteractPlateEntityWithFoodEntity(player, _itemEntity as PlateEntity, player.CurrentItem as FoodEntity);
                    break;
                case KitchenItemEntity:
                    if (_itemEntity is FoodEntity) InteractKitchenItemEntityWithFoodEntity(player, player.CurrentItem as KitchenItemEntity, _itemEntity as FoodEntity);
                    if (_itemEntity is PlateEntity) InteractPlateEntityWithKitchenItemEntity(player, _itemEntity as PlateEntity, player.CurrentItem as KitchenItemEntity);
                    break;
                case PlateEntity:
                    if (_itemEntity is FoodEntity) InteractPlateEntityWithFoodEntity(player, player.CurrentItem as PlateEntity, _itemEntity as FoodEntity);
                    if (_itemEntity is KitchenItemEntity) InteractPlateEntityWithKitchenItemEntity(player, player.CurrentItem as PlateEntity, _itemEntity as KitchenItemEntity);
                    break;
            }
        }
    }
    private void TakeItemFromPlayer(ChefController player)
    {
        Item = player.CurrentItem;
        player.CurrentItem = null;
    }
    private void GivePlayerItemFromTable(ChefController player)
    {
        player.CurrentItem = _itemEntity;
        _itemEntity = null;
    }
    
    /// <summary>
    /// Combine FoodEntity on the table with another one from other holder
    /// If it can Combine
    /// </summary>
    /// <param name="playerItem">Item which will combined with another one</param>
    private void InteractFoodEntities(ChefController player, FoodEntity playerItem)
    {
        // Convert ItemEntity to FoodItem
        FoodEntity foodEntity = _itemEntity as FoodEntity;
        FoodEntity combinedFoodItem;

        // Check for a Combinations
        if (foodEntity.CanCombine(playerItem, out combinedFoodItem))
        {
            // Create new Item and put it on the Table
            Item = combinedFoodItem;
            player.CurrentItem = null;
        }
    }

    /// <summary>
    /// Interact Kitchen Item with Food Entity
    /// </summary>
    /// <param name="player">who interact with this block</param>
    private void InteractKitchenItemEntityWithFoodEntity(ChefController player, KitchenItemEntity kitchenItemEntity, FoodEntity foodEntity)
    {
        ItemEntity playerItem = player.CurrentItem;
        // We try to add item to Kitchen Item if that's right return
        if (kitchenItemEntity.TryToAddItem(foodEntity))
        {
            // If ingredient was added set player CurrentItem to null and set animation to Idle
            if (playerItem is not KitchenItemEntity) player.CurrentItem = null;
            return;
        }
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
        } 
        else
        {
            player.CurrentItem = combinedFoodItem;
        }
        // Clear cooked item on Kitchen Item
        kitchenItemEntity.RemoveItems();
    }

    private void InteractPlateEntityWithFoodEntity(ChefController player, PlateEntity plateEntity, FoodEntity foodEntity)
    {
        bool foodEntityWasAdded = plateEntity.TryToAddItemOn(foodEntity);
        if (!foodEntityWasAdded) return;

        if (player.CurrentItem is PlateEntity)
        {
            Item = null;
            player.CurrentItem = plateEntity;
        }
        else
        {
            Item = plateEntity;
            player.CurrentItem = null;
        }
    }

    private void InteractPlateEntityWithKitchenItemEntity(ChefController player, PlateEntity plateEntity, KitchenItemEntity kitchenItemEntity)
    {
        FoodEntity cookedEntity = kitchenItemEntity.GetCookedItem();
        if (cookedEntity == null) return;
        cookedEntity.transform.localScale = new Vector3(1f, 1f, 1f);

        bool foodEntityWasAdded = plateEntity.TryToAddItemOn(cookedEntity);
        if (!foodEntityWasAdded) return;

        kitchenItemEntity.RemoveItems(false);

        if (player.CurrentItem is PlateEntity)
        {
            Item = kitchenItemEntity;
            player.CurrentItem = plateEntity;
        }
        else
        {
            Item = plateEntity;
            player.CurrentItem = kitchenItemEntity;
        }
    }
    /// <summary>
    /// Place ItemEntity on Table via using Raycast
    /// </summary>
    /// <param name="item">Item will be placed on a table</param>
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

