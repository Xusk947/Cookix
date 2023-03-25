using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurgerTask", menuName = "Cookix/Item Task/BurgerTask")]

public class BurgerTask : FoodTask
{
    [SerializeField]
    private List<FoodItem> foodItems;

    public override bool Compare(FoodEntity item)
    {
        // Check if Item equal to main item
        if (item.item != _foodItem) return false;
        // Check for count of list in item and if it size equal to task list
        if (item.addedItems.Count != foodItems.Count) return false;
        // Check for each item and if all of them is equal to main sequence, pass the task
        for (int i = 0; i < foodItems.Count; i++)
        {
            if (foodItems[i] != item.addedItems[i].Input) return false;
        }
        return true;
    }
}
