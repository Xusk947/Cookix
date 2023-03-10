using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEntity : ItemEntity
{
    public List<CombineData> addedItems = new List<CombineData>();
    public bool CanCombine(FoodEntity itemEntity, out FoodEntity combined)
    {
        combined = null;

        foreach(CombineData combineData in foodItem.CombineWith)
        {
            // If this item requirments is the same as an input item then we check for Combining Requires
            if (combineData.Input == itemEntity.item)
            {
                addedItems.Add(combineData);
                combined = foodItem.Merge(this, itemEntity, combineData);
                return true;
            }
        }

        foreach(CombineData combineData in itemEntity.foodItem.CombineWith)
        {
            if (combineData.Input == item) 
            {
                itemEntity.addedItems.Add(combineData);
                combined = itemEntity.foodItem.Merge(itemEntity, this, combineData);
                return true;
            }
        }

        return false;
    }

    public FoodItem foodItem { get { return item as FoodItem; } }
}
