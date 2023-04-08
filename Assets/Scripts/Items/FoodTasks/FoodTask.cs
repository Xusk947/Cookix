using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemTask", menuName = "Cookix/Item Task/ItemTask")]
public class FoodTask : ScriptableObject
{
    public float Score = 50f;
    public float difficult = 1f;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    protected FoodItem _foodItem;
    
    public Sprite Icon { get { return _icon; } }
    public FoodItem FoodItem { get { return _foodItem; } }

    public virtual bool Compare(FoodEntity item)
    {
        return item.foodItem == _foodItem;
    }

    public virtual List<FoodItem> GetIngredients()
    {
        return new List<FoodItem>() { _foodItem };
    }

    public virtual FoodEntity GetFoodEntity()
    {
        FoodEntity entity = _foodItem.Create(false);
        return entity;
    }
}
