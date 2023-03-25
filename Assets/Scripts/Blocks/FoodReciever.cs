using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodReciever : Block
{
    public override void Interact(PlayerController player)
    {
        if (PlayerItemIsNotFoodEntity(player)) return;
        FoodEntity playerItem = player.CurrentItem as FoodEntity;

        FoodTask finishedTask = FoodTaskManager.Instance.CheckItemForTask(playerItem);
        // Remove player Item if task is finished and later remove this task in TaskManager
        if (finishedTask != null)
        {
            FoodTaskManager.Instance.RemoveTask(finishedTask);
            player.RemoveItem();
        }
    }

    private bool PlayerItemIsNotFoodEntity(PlayerController player)
    {
        if (player.CurrentItem == null) return true;
        if (player.CurrentItem is not FoodEntity) return true;
        return false;
    }
}
