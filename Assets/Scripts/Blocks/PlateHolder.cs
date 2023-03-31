using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateHolder : Block
{
    public PlateItem PlateToSpawn;
    public override void Interact(PlayerController player)
    {
        if (player.CurrentItem == null)
        {
            player.CurrentItem = PlateToSpawn.Create();
        }
        else if (player.CurrentItem is FoodEntity)
        {
            InteractWithFoodEntity(player);
        }
    }

    private void InteractWithFoodEntity(PlayerController player)
    {
        FoodEntity foodEntity = player.CurrentItem as FoodEntity;
        if (!foodEntity.foodItem.canBePlacedOnPlate) return;

        PlateEntity plateEntity = PlateToSpawn.Create();
        if (!plateEntity.TryToAddItemOn(foodEntity))
        {
            Destroy(plateEntity);
        }
        player.CurrentItem = plateEntity;
    }
}
