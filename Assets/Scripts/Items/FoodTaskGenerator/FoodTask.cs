using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodTask
{
    private FoodEntity _item;
    public FoodEntity Item { get { return _item; } }

    public bool Compare(FoodEntity item)
    {
        List<FoodItem> taskList = new List<FoodItem>();
        List<FoodItem> itemList = new List<FoodItem>();

        taskList.AddRange(item.addedItems.ToArray());
        itemList.AddRange(taskList.ToArray());

        Debug.Log(itemList.Count);
        for (int i = 0; i < itemList.Count; i++)
        {
            FoodItem comparable = itemList[i];
            if (taskList.Contains(comparable))
            {
                taskList.Remove(comparable);
            }
        }
        Debug.Log(itemList.Count);

        if (taskList.Count < 1) return true;

        return false;
    }
}
