using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemTask", menuName = "Cookix/Item Task/ItemTask")]
public class FoodTask : ScriptableObject
{
    private FoodEntity _item;

    public FoodEntity Item { get { return _item; } }

    [SerializeField]
    private FoodItem _foodItem;

    public FoodItem FoodItem { get { return _foodItem; } }

    public virtual bool Compare(FoodEntity item)
    {
        return item.foodItem == _foodItem;
    }
}
